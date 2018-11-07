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
	public interface IArticleCategoryService
	{

		ApiResponseModel GetAll();

		ApiResponseModel GetAllWithPagging(int? page, int pageSize);

		ApiResponseModel GetById(int id);

		ArticleCategoryResponseModel GetArticleCategoryById(int id);

		ApiResponseModel Add(ArticleCategory ArticleCategory);

		ApiResponseModel Update(ArticleCategory ArticleCategory);

		ApiResponseModel Delete(int id);

	}

	public class ArticleCategoryService : IArticleCategoryService
	{
		private IArticleCategoryRepository _ArticleCategoryRepository;
		private IUserService _UserService;

		private IUnitOfWork _UnitOfWork;

		public ArticleCategoryService(
			IArticleCategoryRepository ArticleCategoryRepository,
			IUserService UserService,
			IUnitOfWork UnitOfWork)
		{
			this._ArticleCategoryRepository = ArticleCategoryRepository;
			this._UserService = UserService;
			this._UnitOfWork = UnitOfWork;
		}

		/// <summary>
		/// Get all ArticleCategory
		/// </summary>
		/// <returns></returns>
		public ApiResponseModel GetAll()
		{
			var result = new List<ArticleCategoryResponseModel>();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				result = _ArticleCategoryRepository
					.GetAll()
					.Where(m => m.IsDeleted != true)
					.Select(m => new ArticleCategoryResponseModel
					{
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByUser = _UserService.GetUserById(m.CreatedBy),
						Description = m.Description,
						DisplayOrder = m.DisplayOrder,
						ID = m.ID,
						IsDeleted = m.IsDeleted,
						MetaDescriptions = m.MetaDescriptions,
						MetaKeywords = m.MetaKeywords,
						MetaTitle = m.MetaTitle,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByUser = _UserService.GetUserById(m.ModifiedBy),
						Name = m.Name,
						ParentCategory = m.ParentID != null ? GetArticleCategoryById(int.Parse(m.ParentID.ToString())) : null,
						ParentID = m.ParentID,
						PhotoImage = m.PhotoImage
					})
					.ToList();
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
		/// Get all ArticleCategory with pagging
		/// </summary>
		/// <param name="page">Current page</param>
		/// <param name="pageSize">Page size</param>
		/// <returns></returns>
		public ApiResponseModel GetAllWithPagging(int? page, int pageSize)
		{
			var result = new List<ArticleCategoryResponseModel>();
			var paginationSet = new PaginationSet<ArticleCategory>();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var currentPage = page ?? 1;
				int totalRow = 0;
				var query = _ArticleCategoryRepository.GetAll().Where(m => m.IsDeleted != true);
				totalRow = query.Count();

				result = query
					.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
					.Take(pageSize)
					.Select(m => new ArticleCategoryResponseModel
					{
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByUser = _UserService.GetUserById(m.CreatedBy),
						Description = m.Description,
						DisplayOrder = m.DisplayOrder,
						ID = m.ID,
						IsDeleted = m.IsDeleted,
						MetaDescriptions = m.MetaDescriptions,
						MetaKeywords = m.MetaKeywords,
						MetaTitle = m.MetaTitle,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByUser = _UserService.GetUserById(m.ModifiedBy),
						Name = m.Name,
						ParentCategory = m.ParentID != null ? GetArticleCategoryById(int.Parse(m.ParentID.ToString())) : null,
						ParentID = m.ParentID,
						PhotoImage = m.PhotoImage
					})
					.ToList();

				paginationSet = new PaginationSet<ArticleCategory>()
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
		/// Get ArticleCategory by id
		/// </summary>
		/// <param name="id">ID of ArticleCategory</param>
		/// <returns></returns>
		public ApiResponseModel GetById(int id)
		{
			var result = new ArticleCategory();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _ArticleCategoryRepository.GetSingleById(id);
				if (exists != null && !exists.IsDeleted)
				{
					response.Result = GetArticleCategoryById(exists.ID);
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
		/// Get ArticleCategory by id
		/// </summary>
		/// <param name="id">ID of ArticleCategory</param>
		/// <returns></returns>
		public ArticleCategoryResponseModel GetArticleCategoryById(int id)
		{
			try
			{
				var m = _ArticleCategoryRepository.GetSingleById(id);
				if (m != null)
				{
					return new ArticleCategoryResponseModel
					{
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByUser = _UserService.GetUserById(m.CreatedBy),
						Description = m.Description,
						DisplayOrder = m.DisplayOrder,
						ID = m.ID,
						IsDeleted = m.IsDeleted,
						MetaDescriptions = m.MetaDescriptions,
						MetaKeywords = m.MetaKeywords,
						MetaTitle = m.MetaTitle,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByUser = _UserService.GetUserById(m.ModifiedBy),
						Name = m.Name,
						ParentCategory = m.ParentID != null ? GetArticleCategoryById(int.Parse(m.ParentID.ToString())) : null,
						ParentID = m.ParentID,
						PhotoImage = m.PhotoImage
					};
				}
				else
				{
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Add new ArticleCategory
		/// </summary>
		/// <param name="obj">ArticleCategory</param>
		/// <returns></returns>
		public ApiResponseModel Add(ArticleCategory obj)
		{
			var result = new ArticleCategory();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				obj.IsDeleted = false;
				obj.Created = DateTime.Now;
				obj.CreatedBy = 1;//xxx
				obj.Modified = DateTime.Now;
				obj.ModifiedBy = 1;//xxx
				result = _ArticleCategoryRepository.Add(obj);
				_UnitOfWork.Commit();
				response.Message = CommonConstants.AddSuccess;
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
		/// Update ArticleCategory
		/// </summary>
		/// <param name="obj">New ArticleCategory</param>
		/// <returns></returns>
		public ApiResponseModel Update(ArticleCategory obj)
		{
			var result = new ArticleCategory();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _ArticleCategoryRepository.GetSingleById(obj.ID);
				if (exists != null && !exists.IsDeleted)
				{
					exists.Name = obj.Name;
					exists.Description = obj.Description;
					exists.PhotoImage = obj.PhotoImage;
					exists.DisplayOrder = obj.DisplayOrder;
					exists.ParentID = obj.ParentID;
					exists.MetaTitle = obj.MetaTitle;
					exists.MetaDescriptions = obj.MetaDescriptions;
					exists.MetaKeywords = obj.MetaKeywords;
					exists.Modified = DateTime.Now;
					exists.ModifiedBy = 1;//xxx
					_ArticleCategoryRepository.Update(exists);
					_UnitOfWork.Commit();
					response.Result = exists;
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
		/// Delete ArticleCategory by Id
		/// </summary>
		/// <param name="id">ID of ArticleCategory</param>
		/// <returns></returns>
		public ApiResponseModel Delete(int id)
		{
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};
			var exists = _ArticleCategoryRepository.GetSingleById(id);
			if (exists != null && !exists.IsDeleted)
			{
				response.Result = GetArticleCategoryById(id);
				exists.IsDeleted = true;
				_ArticleCategoryRepository.Update(exists);
				_UnitOfWork.Commit();
				response.Message = CommonConstants.DeleteSuccess;
			}
			else
			{
				response.Code = CommonConstants.ApiResponseNotFoundCode;
				response.Message = CommonConstants.NotFoundMessage;
			}
			return response;
		}
	}
}