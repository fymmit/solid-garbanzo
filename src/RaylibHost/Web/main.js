import { dotnet } from './_framework/dotnet.js';

const canvas = document.getElementById('game');
const status = document.getElementById('status');

try {
    const { getAssemblyExports, getConfig, runMain } = await dotnet
        .withDiagnosticTracing(false)
        .create();

    const config = getConfig();
    const exports = await getAssemblyExports(config.mainAssemblyName);

    // Raylib's Emscripten backend creates its WebGL context on this canvas.
    dotnet.instance.Module.canvas = canvas;
    await runMain();

    const host = exports.RaylibHost.Web.Host;
    status.hidden = true;
    canvas.focus();

    function frame() {
        requestAnimationFrame(frame);
        host.UpdateFrame();
    }

    requestAnimationFrame(frame);
} catch (error) {
    console.error(error);
    status.textContent = 'Unable to start the game. See the browser console for details.';
    status.classList.add('error');
}
