document.getElementById('search-part').addEventListener('submit', function (event) {
    event.preventDefault(); // Prevent the form from submitting

    const searchTerm = document.getElementById('partNameSearch').value.toLowerCase();
    const image = document.getElementById('flying-image');

    if (searchTerm === 'shemon') {
        image.style.display = 'block'; // Show the image

        // Delay to allow the browser to render the element as visible before starting the animation
        setTimeout(() => {
            image.style.transform = 'translate(calc(100vw - 50%), -50%)'; // Move across the viewport
        }, 50); // Small delay to trigger transition

        // Reset the image position after animation completes
        setTimeout(() => {
            image.style.display = 'none'; // Hide the image
            image.style.transform = 'translate(-50%, -50%)'; // Reset position
        }, 10000); // Match this time to the CSS transition duration
    }
});