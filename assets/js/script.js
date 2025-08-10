document.addEventListener('DOMContentLoaded', function() {
  // 主题切换功能
  const themeToggle = document.getElementById('themeToggle');
  const savedTheme = localStorage.getItem('theme');
  
  if (savedTheme) {
    document.documentElement.setAttribute('data-theme', savedTheme);
    themeToggle.textContent = savedTheme === 'dark' ? '☀️' : '🌙';
  }
  
  themeToggle.addEventListener('click', () => {
    const currentTheme = document.documentElement.getAttribute('data-theme');
    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
    
    document.documentElement.setAttribute('data-theme', newTheme);
    themeToggle.textContent = newTheme === 'dark' ? '☀️' : '🌙';
    localStorage.setItem('theme', newTheme);
  });
  
  // 移动端导航菜单切换
  const navbarToggle = document.getElementById('navbarToggle');
  const navbar = document.querySelector('.navbar');
  
  if (navbarToggle) {
    navbarToggle.addEventListener('click', () => {
      navbar.classList.toggle('active');
    });
  }
  
  // 为所有文章链接添加点击动画
  const postLinks = document.querySelectorAll('.post-title a');
  postLinks.forEach(link => {
    link.addEventListener('click', function(e) {
      e.preventDefault();
      
      // 添加点击动画
      this.classList.add('clicked');
      setTimeout(() => {
        window.location.href = this.href;
      }, 300);
    });
  });
  
  // 初始化页面
  console.log('博客已加载！');
});
