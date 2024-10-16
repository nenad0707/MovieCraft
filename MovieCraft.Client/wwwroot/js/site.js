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

window.removeSplashScreen = () => {
    const splash = document.getElementById('splash-screen');
    if (splash) {
        splash.style.opacity = '0';
        splash.style.pointerEvents = 'none';
        setTimeout(() => splash.remove(), 500);
    }
};

window.preloadImage = (url) => {
    return new Promise((resolve) => {
        const img = new Image();
        img.src = url;
        img.onload = () => resolve(true);
    });
};
