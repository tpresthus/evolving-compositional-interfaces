// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelectorAll('.flipper').forEach(function(flipper) {
    flipper.addEventListener('click', function(e) {
        var node = this;

        while (!node.classList.contains('flip-card')) {
            node = node.parentNode;

            if (!node.classList) {
                console.error('No element with class "flip-card" found.');
                return;
            }
        }

        node.classList.toggle('is-flipped');
    });
});
