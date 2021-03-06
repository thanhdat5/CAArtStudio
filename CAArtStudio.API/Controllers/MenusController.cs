﻿using CAArtStudio.API.Infrastructure.Core;
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
	[RoutePrefix("api/Menus")]
	public class MenuController : ApiControllerBase
	{
		#region Initialize
		private IMenuService _MenuService;

		public MenuController(IErrorService errorService, IMenuService MenuService)
			: base(errorService)
		{
			this._MenuService = MenuService;
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
			return _MenuService.GetAll();
		}

		[Route("getallwithpaging")]
		[HttpGet]
		public ApiResponseModel GetAllWithPagging(int page, int pageSize)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _MenuService.GetAllWithPagging(page, pageSize);
		}

		[Route("getbyid/{id:int}")]
		[HttpGet]
		public ApiResponseModel GetById(int id)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _MenuService.GetById(id);
		}

		[Route("create")]
		[HttpPost]
		public ApiResponseModel Create(Menu obj)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _MenuService.Add(obj);
		}

		[Route("update")]
		[HttpPut]
		public ApiResponseModel Update(Menu obj)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _MenuService.Update(obj);
		}

		[Route("delete/{id:int}")]
		[HttpDelete]
		public ApiResponseModel Delete(int id)
		{
			//if (HttpContext.Current.Session["UserLogged"] == null)
			//{
			//	return CommonConstants.accessDenied;
			//}
			return _MenuService.Delete(id);
		}
	}
}
