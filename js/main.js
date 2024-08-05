document.addEventListener('DOMContentLoaded', function() {
    const links = document.querySelectorAll('.header-nav a');
    
    // 为每个链接添加点击事件
    links.forEach(link => {
        link.addEventListener('click', function(event) {
            event.preventDefault(); // 阻止默认行为
            const newTitle = this.getAttribute('data-title'); // 获取data-title属性值
            document.title = newTitle; // 更改文档标题
        });
    });
});