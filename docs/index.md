---
# https://vitepress.dev/reference/default-theme-home-page
layout: home

hero:
    name: Indago
    text: Compile-time assembly scanning for .NET
    tagline: AOT safe. Zero reflection. Build-time performance.
    actions:
        - theme: brand
          text: Get Started
          link: /guide/
        - theme: alt
          text: API Reference
          link: /reference/iindago-provider

features:
    - title: AOT Safe
      details: All type scanning is resolved at compile time by a Roslyn source generator. Zero reflection APIs in the generated output — fully compatible with .NET Native AOT and IL trimming.
    - title: Build-Time Scanning
      details: Instead of walking assemblies at startup, Indago evaluates your selector expressions during the build and emits a strongly-typed, pre-computed provider class.
    - title: Minimal API Surface
      details: One interface (IIndagoProvider), a few attributes, and a DI registration helper. Designed to do one thing well and stay out of your way.
---
