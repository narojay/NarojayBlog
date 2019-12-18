﻿using System;
using AutoMapper;
using Markdig;
using NarojayBlog.DatabaseRepository.Model;
using NarojayBlog.IManager;
using NarojayBlog.ManagerEntity;
using NarojayBlog.Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NarojayBlog.Core;

namespace NarojayBlog.Manager
{
    public class ArticleManager : BaseManager<Article, ArticleEntity, ArticleRepository>, IArticleManager
    {
        private readonly ILogger _logger;

        public ArticleManager(ArticleRepository articleRepository, IMapper mapper,ILogger<ArticleManager> logger ) : base(articleRepository, mapper)
        {
            _logger = logger;
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
