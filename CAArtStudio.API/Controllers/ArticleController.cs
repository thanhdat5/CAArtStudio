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
	[RoutePrefix("api/Articles")]
	public class ArticleController : ApiControllerBase
	{
		#region Initialize
		private IArticleService _ArticleService;

		public ArticleController(IErrorService errorService, IArticleService ArticleService)
			: base(errorService)
		{
			this._ArticleService = ArticleService;
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
			return _ArticleService.GetAll();
		}

		[Route("getallwithpaging")]
		[HttpGet]
		public ApiResponseModel GetAllWithPagging(int page, int pageSize)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleService.GetAllWithPagging(page, pageSize);
		}

		[Route("getbyid/{id:int}")]
		[HttpGet]
		public ApiResponseModel GetById(int id)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleService.GetById(id);
		}

		[Route("create")]
		[HttpPost]
		public ApiResponseModel Create(Article obj)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleService.Add(obj);
		}

		[Route("update")]
		[HttpPut]
		public ApiResponseModel Update(Article obj)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleService.Update(obj);
		}

		[Route("delete/{id:int}")]
		[HttpDelete]
		public ApiResponseModel Delete(int id)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleService.Delete(id);
		}

		[Route("updateviewcount/{id:int}")]
		[HttpDelete]
		public ApiResponseModel UpdateViewCount(int id)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _ArticleService.UpdateViewCount(id);
		}
	}
}
