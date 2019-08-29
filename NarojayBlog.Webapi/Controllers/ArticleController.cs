﻿using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NarojayBlog.IManager;
using NarojayBlog.ManagerEntity;
using NarojayBlog.ViewModel;

namespace NarojayBlog.Webapi.Controllers
{
    public class ArticleController : BaseController

    {
        private readonly IArticleManager _articleManager;

        public ArticleController(IArticleManager articleManager, IMapper mapper) : base(mapper)
        {
            _articleManager = articleManager;
        }

        [HttpGet("WordsNumber")]
        public IActionResult GetWordsNumber()
        {
            try
            {
                var numbers = _articleManager.CalculateArticleWordsNumber();
                return Ok(numbers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Title")]
        public IActionResult GetTitleNumber()
        {
            try
            {
                var numbers = _articleManager.GetArticleNumber();
                return Ok(numbers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Articles")]
        public IActionResult GetArticles()
        {
            try
            {
                var articleEntities = _articleManager.GetArticles();
                if (!articleEntities.Any())
                {
                    return NoContent();
                }

                return Ok(articleEntities);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        ///
        [HttpGet("Article/{id}")]
        public IActionResult GetArticle(string id)
        {
            try
            {
                var articleEntity = _articleManager.GetArticleById(id);
                if (articleEntity == null)
                {
                    return NoContent();
                }
                return Ok(articleEntity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult AddArticle(ArticleAddViewModel articleAddViewModel)
        {
            try
            {
                var result = _articleManager.AddArticle(Mapper.Map<ArticleEntity>(articleAddViewModel));
                if (!result)
                {
                    return BadRequest(new {Code = 1002, Content = "文章添加失败"});
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
