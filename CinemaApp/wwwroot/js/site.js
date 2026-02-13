document.addEventListener("DOMContentLoaded", function () {

    let current = 0;
    const slides = document.querySelectorAll('.slide');
    const numbers = document.querySelectorAll('.slide-numbers span');

    if (slides.length === 0) return;

    function showSlide(index) {
        // update slides
        slides.forEach(s => s.classList.remove('active'));
        slides[index].classList.add('active');

        // update numbers
        numbers.forEach(n => n.classList.remove('active'));
        if (numbers[index]) {
            numbers[index].classList.add('active');
        }
    }

    function nextSlide() {
        current = (current + 1) % slides.length;
        showSlide(current);
    }

    function prevSlide() {
        current = (current - 1 + slides.length) % slides.length;
        showSlide(current);
    }

    // expose buttons to HTML
    window.nextSlide = nextSlide;
    window.prevSlide = prevSlide;

    // auto slide
    setInterval(nextSlide, 5000);
});
