using CAArtStudio.API.Infrastructure.Core;
using CAArtStudio.Common;
using CAArtStudio.Model;
using CAArtStudio.Model.ResponseModel;
using CAArtStudio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CAArtStudio.API.Controllers
{
	[RoutePrefix("api/ArticleCategories")]
	public class ArticleCategoryController : ApiControllerBase
	{
		#region Initialize
		private IArticleCategoryService _ArticleCategoryService;

		public ArticleCategoryController(IErrorService errorService, IArticleCategoryService ArticleCategoryService)
			: base(errorService)
		{
			this._ArticleCategoryService = ArticleCategoryService;
		}

		#endregion

		[Route("getall")]
		[HttpGet]
		public ApiResponseModel GetAll()
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleCategoryService.GetAll();
		}

		[Route("getallwithpaging")]
		[HttpGet]
		public ApiResponseModel GetAllWithPagging(int page, int pageSize)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleCategoryService.GetAllWithPagging(page, pageSize);
		}

		[Route("getbyid/{id:int}")]
		[HttpGet]
		public ApiResponseModel GetById(int id)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleCategoryService.GetById(id);
		}

		[Route("create")]
		[HttpPost]
		public ApiResponseModel Create(ArticleCategory obj)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleCategoryService.Add(obj);
		}

		[Route("update")]
		[HttpPut]
		public ApiResponseModel Update(ArticleCategory obj)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleCategoryService.Update(obj);
		}

		[Route("delete/{id:int}")]
		[HttpDelete]
		public ApiResponseModel Delete(int id)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleCategoryService.Delete(id);
		}
	}
}
