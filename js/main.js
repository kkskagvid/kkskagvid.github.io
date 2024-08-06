document.addEventListener('DOMContentLoaded', function() {
    const tableBody = document.querySelector('table tbody');

    fetch('https://kkskagvid.github.io/json/data.json') // 确保路径正确
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            data.forEach(file => {
                const row = document.createElement('tr');
                const nameCell = document.createElement('td');
                const sizeCell = document.createElement('td');
                const lastModifiedCell = document.createElement('td');

                if (file.path == "default"){
                    nameCell.innerHTML = `<a href="/downloads/${file.name}" download>${file.name}</a>`;
                } else {
                    nameCell.innerHTML = `<a href="${file.path}" download>${file.name}</a>`;
                }
                
                sizeCell.textContent = file.size;
                lastModifiedCell.textContent = file.lastModified;

                row.appendChild(nameCell);
                row.appendChild(sizeCell);
                row.appendChild(lastModifiedCell);

                tableBody.appendChild(row);
            });
        })
        .catch(error => {
            console.error('Error fetching or parsing files:', error);
            // 可选：向用户显示错误消息
            alert('无法加载文件列表，请稍后再试。');
        });
});
