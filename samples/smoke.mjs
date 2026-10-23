#!/usr/bin/env node
// Runs a built Specular sample and checks the SPECULAR-SAMPLE line it prints (samples/hosts/Shared/SampleScan.cs).
// No npm dependencies, so it runs the same on Linux, macOS and Windows runners.
//
//   node samples/smoke.mjs exe -- <command> [args...]
//       Runs the command; passes when it exits 0 and printed a MATCH line.
//   node samples/smoke.mjs web --url <url> -- <command> [args...]
//       Starts the server, waits for <url> to answer, and passes when it printed a MATCH line and <url>'s body contains one.
//   node samples/smoke.mjs browser <directory>
//       Serves <directory> (a published wwwroot), opens it in headless Chrome and passes when the page's console logs a MATCH
//       line. Chrome comes from CHROME_PATH, or the usual install locations.

import { spawn } from 'node:child_process';
import { existsSync, mkdtempSync, readFileSync, rmSync, statSync } from 'node:fs';
import { createServer } from 'node:http';
import { tmpdir } from 'node:os';
import { extname, join, normalize, resolve, sep } from 'node:path';
import { setTimeout as delay } from 'node:timers/promises';

const PREFIX = 'SPECULAR-SAMPLE:';
const MATCH = `${PREFIX} MATCH`;
const TIMEOUT_MS = Number(process.env.SPECULAR_SAMPLE_TIMEOUT_MS ?? 180_000);

function commandAfterSeparator(args) {
    const index = args.indexOf('--');
    const command = index < 0 ? [] : args.slice(index + 1);
    if (command.length === 0) throw new Error('missing "-- <command> [args...]"');
    return command;
}

function option(args, name) {
    const index = args.indexOf(name);
    if (index < 0 || index + 1 >= args.length) throw new Error(`missing ${name} <value>`);
    return args[index + 1];
}

function start([command, ...args], { env = {}, echo = true } = {}) {
    const child = spawn(command, args, { env: { ...process.env, ...env }, stdio: ['ignore', 'pipe', 'pipe'] });
    let output = '';
    for (const stream of [child.stdout, child.stderr]) {
        stream.setEncoding('utf8');
        stream.on('data', chunk => {
            output += chunk;
            if (echo) process.stdout.write(chunk);
        });
    }
    const exited = new Promise((resolveExit, rejectExit) => {
        child.on('error', rejectExit);
        child.on('exit', code => resolveExit(code));
    });
    return { child, exited, output: () => output };
}

function resultLine(text) {
    return text.split(/\r?\n/).find(line => line.includes(PREFIX));
}

async function withTimeout(promise, what) {
    let timer;
    const timeout = new Promise((_, rejectTimeout) => {
        timer = setTimeout(() => rejectTimeout(new Error(`timed out after ${TIMEOUT_MS} ms waiting for ${what}`)), TIMEOUT_MS);
    });
    try {
        return await Promise.race([promise, timeout]);
    } finally {
        clearTimeout(timer);
    }
}

async function runExe(command) {
    const run = start(command, { env: { SPECULAR_SAMPLE_SMOKE: '1' } });
    const code = await withTimeout(run.exited, 'the process to exit');
    const line = resultLine(run.output());
    if (!line?.includes(MATCH)) throw new Error(`no MATCH line (got: ${line ?? 'nothing'})`);
    if (code !== 0) throw new Error(`exit code ${code}`);
}

async function runWeb(url, command) {
    const run = start(command);
    try {
        const body = await withTimeout(
            (async () => {
                for (;;) {
                    const exited = await Promise.race([run.exited, delay(500, 'running')]);
                    if (exited !== 'running') throw new Error(`server exited with code ${exited} before answering`);
                    try {
                        const response = await fetch(url);
                        if (response.ok) return await response.text();
                    } catch {
                        // Not listening yet.
                    }
                }
            })(),
            url,
        );
        const line = resultLine(run.output());
        if (!line?.includes(MATCH)) throw new Error(`no MATCH line in the server output (got: ${line ?? 'nothing'})`);
        if (!body.includes(MATCH)) throw new Error(`${url} answered without a MATCH line: ${body}`);
    } finally {
        run.child.kill();
    }
}

const contentTypes = {
    '.html': 'text/html; charset=utf-8',
    '.js': 'text/javascript',
    '.mjs': 'text/javascript',
    '.json': 'application/json',
    '.wasm': 'application/wasm',
    '.css': 'text/css',
    '.svg': 'image/svg+xml',
    '.png': 'image/png',
    '.ico': 'image/x-icon',
    '.ttf': 'font/ttf',
    '.woff2': 'font/woff2',
    '.webmanifest': 'application/manifest+json',
};

function serve(directory) {
    const root = resolve(directory);
    if (!existsSync(join(root, 'index.html'))) throw new Error(`${root} has no index.html`);
    const server = createServer((request, response) => {
        const path = decodeURIComponent(new URL(request.url ?? '/', 'http://localhost').pathname);
        let file = normalize(join(root, path));
        if (file !== root && !file.startsWith(root + sep)) {
            response.writeHead(403).end();
            return;
        }
        if (!existsSync(file) || statSync(file).isDirectory()) {
            // Client-side routes fall back to the app's page.
            file = extname(path) === '' ? join(root, 'index.html') : '';
        }
        if (!file) {
            response.writeHead(404).end();
            return;
        }
        response.writeHead(200, {
            'Content-Type': contentTypes[extname(file).toLowerCase()] ?? 'application/octet-stream',
            'Cache-Control': 'no-store',
        });
        response.end(readFileSync(file));
    });
    return new Promise(resolveServer => server.listen(0, '127.0.0.1', () => resolveServer(server)));
}

function findChrome() {
    if (process.env.CHROME_PATH) return process.env.CHROME_PATH;
    const candidates = {
        darwin: ['/Applications/Google Chrome.app/Contents/MacOS/Google Chrome', '/Applications/Chromium.app/Contents/MacOS/Chromium'],
        win32: [
            join(process.env.PROGRAMFILES ?? 'C:\\Program Files', 'Google', 'Chrome', 'Application', 'chrome.exe'),
            join(process.env['PROGRAMFILES(X86)'] ?? 'C:\\Program Files (x86)', 'Google', 'Chrome', 'Application', 'chrome.exe'),
            join(process.env.LOCALAPPDATA ?? '', 'Google', 'Chrome', 'Application', 'chrome.exe'),
        ],
        linux: ['/usr/bin/google-chrome', '/usr/bin/google-chrome-stable', '/usr/bin/chromium', '/usr/bin/chromium-browser'],
    }[process.platform] ?? [];
    const chrome = candidates.find(candidate => existsSync(candidate));
    if (!chrome) throw new Error('Chrome not found; set CHROME_PATH');
    return chrome;
}

// A minimal Chrome DevTools Protocol client over the WebSocket Node ships with. Every wait is bounded: a Chrome that stops
// answering would otherwise hang the job until GitHub cancels it.
async function connect(webSocketUrl) {
    const socket = new WebSocket(webSocketUrl);
    await withTimeout(
        new Promise((resolveOpen, rejectOpen) => {
            socket.addEventListener('open', resolveOpen, { once: true });
            socket.addEventListener('error', () => rejectOpen(new Error(`can't connect to ${webSocketUrl}`)), { once: true });
        }),
        `the DevTools connection to ${webSocketUrl}`,
    );
    let nextId = 0;
    const pending = new Map();
    const listeners = [];
    socket.addEventListener('message', ({ data }) => {
        const message = JSON.parse(data);
        if (message.id !== undefined) {
            const call = pending.get(message.id);
            pending.delete(message.id);
            if (message.error) call?.reject(new Error(message.error.message));
            else call?.resolve(message.result);
        } else {
            for (const listener of listeners) listener(message);
        }
    });
    return {
        send: (method, params = {}, sessionId = undefined) =>
            withTimeout(
                new Promise((resolveCall, rejectCall) => {
                    const id = ++nextId;
                    pending.set(id, { resolve: resolveCall, reject: rejectCall });
                    socket.send(JSON.stringify({ id, method, params, sessionId }));
                }),
                `Chrome to answer ${method}`,
            ),
        on: listener => listeners.push(listener),
        close: () => socket.close(),
    };
}

async function runBrowser(directory) {
    if (!directory) throw new Error('missing <directory>');
    const server = await serve(directory);
    const url = `http://127.0.0.1:${server.address().port}/`;
    const profile = mkdtempSync(join(tmpdir(), 'specular-smoke-'));
    const args = [
        '--headless=new',
        '--disable-gpu',
        '--no-first-run',
        '--no-default-browser-check',
        '--remote-debugging-port=0',
        `--user-data-dir=${profile}`,
        // Ubuntu runners restrict the unprivileged user namespaces Chrome's sandbox needs.
        ...(process.platform === 'linux' ? ['--no-sandbox'] : []),
        'about:blank',
    ];
    // Chrome's own logging is noise; the page's console comes through the DevTools protocol below.
    const chrome = start([findChrome(), ...args], { echo: false });
    let browser;
    try {
        const portFile = join(profile, 'DevToolsActivePort');
        // Chrome writes the port and the browser endpoint's path; until both are there the file may be missing, half written or,
        // on Windows, still locked (EBUSY).
        const readPortFile = () => {
            try {
                const [port, path] = readFileSync(portFile, 'utf8').split(/\r?\n/);
                return port && path ? [port, path] : undefined;
            } catch {
                return undefined;
            }
        };
        const [port, path] = await withTimeout(
            (async () => {
                for (;;) {
                    const lines = readPortFile();
                    if (lines) return lines;
                    await delay(100);
                }
            })(),
            'Chrome to start',
        );
        browser = await connect(`ws://127.0.0.1:${port}${path}`);

        let resolveLine;
        const lineLogged = new Promise(resolveFound => (resolveLine = resolveFound));
        const log = text => {
            console.log(`  [browser] ${text}`);
            if (text.includes(PREFIX)) resolveLine(resultLine(text));
        };
        browser.on(({ method, params }) => {
            if (method === 'Runtime.consoleAPICalled') {
                log(params.args.map(arg => arg.value ?? arg.description ?? '').join(' '));
            } else if (method === 'Runtime.exceptionThrown') {
                log(`uncaught: ${params.exceptionDetails.exception?.description ?? params.exceptionDetails.text}`);
            } else if (method === 'Log.entryAdded') {
                log(`${params.entry.level}: ${params.entry.text}`);
            }
        });

        const { targetId } = await browser.send('Target.createTarget', { url: 'about:blank' });
        const { sessionId } = await browser.send('Target.attachToTarget', { targetId, flatten: true });
        await browser.send('Runtime.enable', {}, sessionId);
        await browser.send('Log.enable', {}, sessionId);
        await browser.send('Page.navigate', { url }, sessionId);

        const line = await withTimeout(lineLogged, `a ${PREFIX} line from ${url}`);
        if (!line.includes(MATCH)) throw new Error(line);
    } finally {
        await Promise.race([browser?.send('Browser.close').catch(() => {}), delay(5_000)]);
        browser?.close();
        chrome.child.kill();
        await Promise.race([chrome.exited, delay(5_000)]);
        if (chrome.child.exitCode === null && chrome.child.signalCode === null) chrome.child.kill('SIGKILL');
        // Chrome's keep-alive connections would otherwise hold the server (and this process) open.
        server.closeAllConnections();
        server.close();
        try {
            rmSync(profile, { recursive: true, force: true, maxRetries: 5, retryDelay: 200 });
        } catch {
            // A Chrome process still holding the profile on Windows; the runner's temp directory is thrown away anyway.
        }
    }
}

// Last, so the declarations above are initialized before anything runs.
const [mode, ...rest] = process.argv.slice(2);
try {
    switch (mode) {
        case 'exe':
            await runExe(commandAfterSeparator(rest));
            break;
        case 'web':
            await runWeb(option(rest, '--url'), commandAfterSeparator(rest));
            break;
        case 'browser':
            await runBrowser(rest[0]);
            break;
        default:
            throw new Error(`unknown mode '${mode ?? ''}' (expected exe, web or browser)`);
    }
    console.log(`smoke: PASS (${mode})`);
} catch (error) {
    console.error(`smoke: FAIL (${mode}): ${error instanceof Error ? error.message : error}`);
    process.exitCode = 1;
}
// A child the sample left behind (Chrome's helper processes, a server's workers) can keep the event loop alive and hang the job;
// give output a moment to flush, then exit regardless.
setTimeout(() => process.exit(), 5_000).unref();
