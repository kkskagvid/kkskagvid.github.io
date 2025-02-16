// 背景图片列表
let backgroundImages = [
    "url('../img/background/large_eva04_a.jpg')",
    "url('../img/background/large_eva04_b.jpg')",
    "url('../img/background/large_eva04_c.jpg')",
    "url('../img/background/large_eva04_d.jpg')",
    "url('../img/background/large_eva04_e.jpg')",
    "url('../img/background/large_eva04_f.jpg')",
    "url('../img/background/large_eva04_g.jpg')",
    "url('../img/background/large_eva04_h.jpg')",

    "url('../img/background/large_eva06_a.jpg')",
    "url('../img/background/large_eva06_b.jpg')",
    "url('../img/background/large_eva06_c.jpg')",
    "url('../img/background/large_eva06_d.jpg')",
    "url('../img/background/large_eva06_e.jpg')",
    "url('../img/background/large_eva06_f.jpg')",
    "url('../img/background/large_eva06_g.jpg')",

    "url('../img/background/large_eva07_a.jpg')",
    "url('../img/background/large_eva07_b.jpg')",
    "url('../img/background/large_eva07_c.jpg')",
    "url('../img/background/large_eva07_d.jpg')",
    "url('../img/background/large_eva07_e.jpg')",
    "url('../img/background/large_eva07_f.jpg')",

    "url('../img/background/large_eva08_a.jpg')",
    "url('../img/background/large_eva08_b.jpg')",
    "url('../img/background/large_eva08_c.jpg')",
    "url('../img/background/large_eva08_d.jpg')",
    "url('../img/background/large_eva08_e.jpg')",
    "url('../img/background/large_eva08_f.jpg')"
];

let currentImageIndex = 0;

function changeBackground() {
    const nextImageIndex = (currentImageIndex + 1) % backgroundImages.length;
    
    // 更新body的伪元素::before的背景图片
    document.body.style.setProperty('--bg-image', backgroundImages[nextImageIndex]);
    
    currentImageIndex = nextImageIndex;
}

// 初始设置第一个背景图片
document.body.style.setProperty('--bg-image', backgroundImages[currentImageIndex]);

// 每隔6(Debug)/30(Release)秒切换一次背景图片
setInterval(changeBackground, 30000);

// 在CSS中使用自定义属性来设置背景图片
document.body.style.setProperty('--bg-image', '');