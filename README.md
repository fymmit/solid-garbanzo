# solid-garbanzo

A game

`src/Game` contains the game rules and desktop entry point, while `src/Core`
provides the entity framework and Raylib-backed input and rendering. `src/WebHost`
hosts the game in the browser through WebAssembly.

## Run locally

```sh
./run_game.sh
```

## Build the web version

The browser version uses Raylib's WebAssembly/WebGL backend through Raylib-cs.

```sh
dotnet publish src/WebHost/WebHost.csproj -c Release -r browser-wasm --self-contained
```

To test the published bundle locally:

```sh
bunx serve src/WebHost/bin/Release/net10.0/browser-wasm/AppBundle
```

## Deploy to itch.io

Pushes to `main` build and deploy the WebAssembly bundle to
[`fymmit/solid-garbanzo`](https://fymmit.itch.io/solid-garbanzo) through the
`html5` channel. The workflow can also be run manually from the Actions tab.

Before the first deployment, add an itch.io API key with the `wharf` scope as
the `BUTLER_API_KEY` repository Actions secret. Generate the key through the
[itch.io API keys settings](https://itch.io/user/settings/api-keys) page, or by
running `butler login` locally and retrieving the resulting Butler credential.

After the first upload, configure the itch.io game page as **HTML** and mark
the `html5` upload as **HTML5 / Playable in browser**.
