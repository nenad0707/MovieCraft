window.initSwiper = function () {
    var swiper = new Swiper('.swiper-container', {
        effect: 'coverflow',
        grabCursor: true,
        centeredSlides: true,
        slidesPerView: 'auto',
        initialSlide: 3,
        coverflowEffect: {
            rotate: 50,
            stretch: 0,
            depth: 150,
            modifier: 1.5,
            slideShadows: true,
        },
        loop: true,
        simulateTouch: true,
        navigation: false,
        slideToClickedSlide: true,
    });
};
