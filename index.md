---
layout: home
---

<ul class="post-list">
  {% for post in site.posts %}
    <li class="post-item">
      <h2 class="post-title">
        <a href="{{ post.url }}">{{ post.title }}</a>
      </h2>
      <div class="post-meta">
        <span>📅 {{ post.date | date: "%Y-%m-%d" }}</span>
        <span>⏱️ 阅读时间 {{ post.content | reading_time }}分钟</span>
        <span>👁️ {{ post.content | number_of_words | divided_by: 200.0 | ceil }}次阅读</span>
      </div>
      <p class="post-excerpt">{{ post.excerpt | strip_html | truncate: 200 }}</p>
      <div class="post-tags">
        {% for tag in post.tags %}
          <a href="{{ '/tag/' | append: tag | relative_url }}" class="tag">#{{ tag }}</a>
        {% endfor %}
      </div>
    </li>
  {% endfor %}
</ul>
