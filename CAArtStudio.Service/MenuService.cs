using System;
using System.Collections.Generic;
using System.Linq;
using CAArtStudio.Model.ResponseModel;
using CAArtStudio.Model;
using CAArtStudio.Data.Repositories;
using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Common;

namespace CAArtStudio.Service
{
	public interface IMenuService
	{
		ApiResponseViewModel GetAll();

		ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

		ApiResponseViewModel GetById(int id);

		ApiResponseViewModel Add(Menu Menu);

		ApiResponseViewModel Update(Menu Menu);

		ApiResponseViewModel Delete(int id);

	}

	public class MenuService : IMenuService
	{
		private IMenuRepository _MenuRepository;
		private IUserRepository _UserRepository;

		private IUnitOfWork _UnitOfWork;

		public MenuService(
			IMenuRepository MenuRepository,
			IUserRepository UserRepository,
			IUnitOfWork UnitOfWork)
		{
			this._MenuRepository = MenuRepository;
			this._UserRepository = UserRepository;
			this._UnitOfWork = UnitOfWork;
		}

		/// <summary>
		/// Get all Menu
		/// </summary>
		/// <returns></returns>
		public ApiResponseViewModel GetAll()
		{
			var result = new List<MenuResponseViewModel>();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				result = _MenuRepository
					.GetAll()
					.Where(m => m.IsDeleted != true)
					.Select(m => new MenuResponseViewModel
					{
						ID = m.ID,
						Text = m.Text,
						ParentID = m.ParentID,
						ParentName = m.ParentID != null ? _MenuRepository.GetSingleById(int.Parse(m.ParentID.ToString())).Text : string.Empty,
						Url = m.Url,
						Target = m.Target,
						DisplayOrder = m.DisplayOrder,
						ShowOnHome = m.ShowOnHome,
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByName = _UserRepository.GetSingleById(m.CreatedBy).FullName,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByName = _UserRepository.GetSingleById(m.ModifiedBy).FullName,
					}).ToList();
				response.Result = result;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Get all Menu with pagging
		/// </summary>
		/// <param name="page">Current page</param>
		/// <param name="pageSize">Page size</param>
		/// <returns></returns>
		public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
		{
			var result = new List<MenuResponseViewModel>();
			var paginationSet = new PaginationSet<Menu>();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var currentPage = page ?? 1;
				int totalRow = 0;
				var query = _MenuRepository
					.GetAll()
					.Where(m => m.IsDeleted != true)
					.Select(m => new MenuResponseViewModel
					{
						ID = m.ID,
						Text = m.Text,
						ParentID = m.ParentID,
						ParentName = m.ParentID != null ? _MenuRepository.GetSingleById(int.Parse(m.ParentID.ToString())).Text : string.Empty,
						Url = m.Url,
						Target = m.Target,
						DisplayOrder = m.DisplayOrder,
						ShowOnHome = m.ShowOnHome,
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByName = _UserRepository.GetSingleById(m.CreatedBy).FullName,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByName = _UserRepository.GetSingleById(m.ModifiedBy).FullName,
					});
				totalRow = query.Count();

				result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				paginationSet = new PaginationSet<Menu>()
				{
					Items = result,
					Page = currentPage,
					TotalCount = totalRow,
					TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
				};
				response.Result = paginationSet;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}

		/// <summary>
		/// Get Menu by id
		/// </summary>
		/// <param name="id">ID of Menu</param>
		/// <returns></returns>
		public ApiResponseViewModel GetById(int id)
		{
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var m = _MenuRepository.GetSingleById(id);
				if (m != null && !m.IsDeleted)
				{
					var result = new MenuResponseViewModel
					{
						ID = m.ID,
						Text = m.Text,
						ParentID = m.ParentID,
						ParentName = m.ParentID != null ? _MenuRepository.GetSingleById(int.Parse(m.ParentID.ToString())).Text : string.Empty,
						Url = m.Url,
						Target = m.Target,
						DisplayOrder = m.DisplayOrder,
						ShowOnHome = m.ShowOnHome,
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByName = _UserRepository.GetSingleById(m.CreatedBy).FullName,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByName = _UserRepository.GetSingleById(m.ModifiedBy).FullName,
					};
					response.Result = result;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}

			return response;
		}

		/// <summary>
		/// Add new Menu
		/// </summary>
		/// <param name="obj">Menu</param>
		/// <returns></returns>
		public ApiResponseViewModel Add(Menu obj)
		{
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				obj.Created = DateTime.Now;
				obj.CreatedBy = 1;//xxx
				obj.Modified = DateTime.Now;
				obj.ModifiedBy = 1;//xxx
				_MenuRepository.Add(obj);
				_UnitOfWork.Commit();
				response = GetById(obj.ID);
				response.Message = CommonConstants.AddSuccess;
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;

		}

		/// <summary>
		/// Update Menu
		/// </summary>
		/// <param name="obj">New Menu</param>
		/// <returns></returns>
		public ApiResponseViewModel Update(Menu obj)
		{
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _MenuRepository.GetSingleById(obj.ID);
				if (exists != null && !exists.IsDeleted)
				{
					exists.Text = obj.Text;
					exists.ParentID = obj.ParentID;
					exists.Url = obj.Url;
					exists.Target = obj.Target;
					exists.DisplayOrder = obj.DisplayOrder;
					exists.ShowOnHome = obj.ShowOnHome;
					exists.Modified = DateTime.Now;
					exists.ModifiedBy = 1;//xxx
					_MenuRepository.Update(exists);
					_UnitOfWork.Commit();
					response = GetById(exists.ID);
					response.Message = CommonConstants.SaveSuccess;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}

			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}

			return response;
		}

		/// <summary>
		/// Delete Menu by Id
		/// </summary>
		/// <param name="id">ID of Menu</param>
		/// <returns></returns>
		public ApiResponseViewModel Delete(int id)
		{
			var result = new Menu();
			var response = new ApiResponseViewModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _MenuRepository.GetSingleById(id);
				if (exists != null && !exists.IsDeleted)
				{
					exists.IsDeleted = true;
					exists.Modified = DateTime.Now;
					exists.ModifiedBy = 1;//xxx
					response = GetById(exists.ID);
					_MenuRepository.Update(exists);
					_UnitOfWork.Commit();
					response.Message = CommonConstants.DeleteSuccess;
				}
				else
				{
					response.Code = CommonConstants.ApiResponseNotFoundCode;
					response.Message = CommonConstants.NotFoundMessage;
				}
			}
			catch (Exception ex)
			{
				response.Code = CommonConstants.ApiResponseExceptionCode;
				response.Message = CommonConstants.ErrorMessage + " " + ex.Message;
			}
			return response;
		}
	}
}