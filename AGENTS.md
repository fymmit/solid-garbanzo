# Repository Guide

- Keep this file current when architecture, commands, tooling, lifecycle behavior, or other agent-relevant repository facts change.

## Toolchain and Commands

- Use the .NET 10 SDK. The browser target additionally requires `dotnet workload install wasm-tools`.
- Build all projects with `dotnet build solid-garbanzo.slnx`.
- Run the desktop game with `dotnet run --project src/RaylibHost/RaylibHost.csproj`.
- Publish the browser build with `dotnet publish src/RaylibHost/RaylibHost.csproj -c Release -r browser-wasm --self-contained`; serve `src/RaylibHost/bin/Release/net10.0/browser-wasm/AppBundle/` over HTTP(S), for example with `bunx serve`.
- There are currently no test projects or repo-specific lint/format commands. Use a solution build as the baseline verification; also publish the WASM target when changing `RaylibHost`, its project file, or `Web/`.

## Boundaries

- Dependency direction is `RaylibHost -> Game -> Core`. Keep Raylib input, drawing, window, and platform code in `RaylibHost`; `Game` owns rules and entities; `Core` owns the reusable entity/behaviour lifecycle.
- `GameRuntime` is the host-facing game API. It accepts platform-neutral `GameInput`; do not introduce Raylib types into `Game` or `Core`.

## Lifecycle Gotchas

- `EntityManager.Create` and `Entity.Destroy` queue mutations. Pending removals and additions are applied at the start of the next `GameRuntime.Update`; newly added entities are then activated and their initial behaviours receive `Ready()`.
- Attach behaviours through `Entity.Attach<T>()`, which sets `Behaviour.Parent` and immediately calls `Ready()` only when the entity is already active. Directly editing `Components` bypasses this lifecycle.
- `EntityManager` and the update channels are static. `GameRuntime.Initialize` calls `EntityManager.Setup`; repeated initialization in one process would retain global entities/subscriptions and is not currently a supported reset path.

## Host Targets

- Desktop starts at `RaylibHost.Program.Main`, which owns the loop and closes the window.
- Browser publishing switches startup to `RaylibHost.Web.Host`; `Web/main.js` owns `requestAnimationFrame` and calls the exported `UpdateFrame`. `Program` sets target FPS only outside `BROWSER_WASM`.
- Keep the WASM-specific MSBuild settings in `RaylibHost.csproj`, including the explicit `-O0` Binaryen workaround, unless the installed .NET WASM toolchain has been verified without it.
