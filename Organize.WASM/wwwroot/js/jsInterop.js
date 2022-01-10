window.addEventListener('resize', () => {
    DotNet.invokeMethodAsync('Organize.WASM', 'OnResize');
});

window.blazorDimension = {
    getWidth: () => window.innerWidth
}

window.blazorResize = {
    registerReferenceForResizeEvent: (dotnetReference) => {
        console.log(dotnetReference.assignments);
        window.addEventListener('resize', () => {
            console.log('resize handler from js');
            dotnetReference.invokeMethodAsync('HandleResize', window.innerWidth, window.innerHeight);
        });
    }
}