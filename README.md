# solid-garbanzo

A Raylib game for desktop and the browser.

## Run locally

```sh
dotnet run --project src/Game/Game.csproj
```

## Build the web version

The browser version uses Raylib's WebAssembly/WebGL backend through Raylib-cs.

```sh
dotnet publish src/Game/Game.csproj -c Release -r browser-wasm --self-contained
```

Deploy the complete contents of `src/Game/bin/Release/net10.0/browser-wasm/AppBundle/` to a static web host. The output must be served over HTTP(S), not opened directly from the filesystem.

To test the published bundle locally:

```sh
bunx serve src/Game/bin/Release/net10.0/browser-wasm/AppBundle
```
