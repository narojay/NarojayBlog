﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Markdig;
using Microsoft.Extensions.Logging;
using NarojayBlog.DatabaseRepository.DbContext;
using NarojayBlog.DatabaseRepository.Model;
using NarojayBlog.Manager.Entiy;
using NarojayBlog.Manager.IManager;
using NarojayBlog.Repository.Repository;

namespace NarojayBlog.Manager.Manager
{
    public class ArticleManager : BaseManager<Article, ArticleEntity, ArticleRepository>, IArticleManager
    {
        private readonly ILogger _logger;
        private readonly NarojayContext _context;

        public ArticleManager(ArticleRepository articleRepository, IMapper mapper,ILogger<ArticleManager> logger, NarojayContext context) : base(articleRepository, mapper)
        {
            _logger = logger;
            _context = context;
        }
        public int GetArticleNumber()
        {
            return Repository.GetArticleNumber();
        }
        public int CalculateArticleWordsNumber()
        {
            _logger.LogInformation("calculate the total number of article words");
            return Repository.CalculateWords().Sum();
           
        }

        public IEnumerable<ArticleEntity> GetArticles()
        {
            var articleEntities = GetAll();
            foreach (var articleEntity in articleEntities)
            {
                articleEntity.Content = Markdown.ToHtml(articleEntity.Content);
            }
            

            return articleEntities;
        }

        public ArticleEntity GetArticleById(string id)
        {
            var articleEntity = GetById(id);
            var articleContent = articleEntity.Content;
            articleEntity.Content = Markdown.ToHtml(articleContent);
            return articleEntity;
        }

        public bool AddArticle(ArticleEntity articleEntity)
        {
            return InsertArticle(articleEntity);
        }

        public IEnumerable<ArticleEntity> GetArticles(int page, int size)
        {
            var articleEntities = Repository.GetPage(page, size);
            foreach (var articleEntity in articleEntities)
            {
                articleEntity.Content = Markdown.ToHtml(articleEntity.Content);
            }
            return Mapper.Map<IEnumerable<ArticleEntity>>(articleEntities);
        }

        public async Task<int> TestAsync(int id)
        {
            await Task.Delay(5000);
            return id;
        }


        public string TestException(string id)
        {
            if (id == "测试")
            {
                throw new NullReferenceException("用户不存在");
            }

            return "返回";
        }
    }
}
