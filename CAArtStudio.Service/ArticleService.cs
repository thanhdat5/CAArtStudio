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
	public interface IArticleService
	{

		ApiResponseModel GetAll();

		ApiResponseModel GetAllWithPagging(int? page, int pageSize);

		ApiResponseModel GetById(int id);

		ArticleResponseModel GetArticleById(int id);

		ApiResponseModel Add(Article Article);

		ApiResponseModel Update(Article Article);

		ApiResponseModel Delete(int id);

		ApiResponseModel UpdateViewCount(int id);

	}

	public class ArticleService : IArticleService
	{
		private IArticleRepository _ArticleRepository;
		private IArticleCategoryService _ArticleCategoryService;
		private IUserService _UserService;

		private IUnitOfWork _UnitOfWork;

		public ArticleService(
			IArticleRepository ArticleRepository,
			IUserService UserService,
			IArticleCategoryService ArticleCategoryService,
			IUnitOfWork UnitOfWork)
		{
			this._ArticleRepository = ArticleRepository;
			this._UserService = UserService;
			this._ArticleCategoryService = ArticleCategoryService;
			this._UnitOfWork = UnitOfWork;
		}

		/// <summary>
		/// Get all Article
		/// </summary>
		/// <returns></returns>
		public ApiResponseModel GetAll()
		{
			var result = new List<ArticleResponseModel>();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				result = _ArticleRepository
					.GetAll()
					.Where(m => m.IsDeleted != true)
					.Select(m => new ArticleResponseModel
					{
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByUser = _UserService.GetUserById(m.CreatedBy),
						Description = m.Description,
						ID = m.ID,
						IsDeleted = m.IsDeleted,
						MetaDescriptions = m.MetaDescriptions,
						MetaKeywords = m.MetaKeywords,
						MetaTitle = m.MetaTitle,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByUser = _UserService.GetUserById(m.ModifiedBy),
						PhotoImage = m.PhotoImage,
						CategoryID = m.CategoryID,
						ArticleCategory = m.CategoryID != null ? _ArticleCategoryService.GetArticleCategoryById(int.Parse(m.CategoryID.ToString())) : null,
						Details = m.Details,
						IsActive = m.IsActive,
						IsPopular = m.IsPopular,
						Tags = m.Tags,
						Title = m.Title,
						ViewCount = m.ViewCount
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
		/// Get all Article with pagging
		/// </summary>
		/// <param name="page">Current page</param>
		/// <param name="pageSize">Page size</param>
		/// <returns></returns>
		public ApiResponseModel GetAllWithPagging(int? page, int pageSize)
		{
			var result = new List<ArticleResponseModel>();
			var paginationSet = new PaginationSet<Article>();
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
				var query = _ArticleRepository.GetAll().Where(m => m.IsDeleted != true);
				totalRow = query.Count();

				result = query
					.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
					.Take(pageSize)
					.Select(m => new ArticleResponseModel
					{
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByUser = _UserService.GetUserById(m.CreatedBy),
						Description = m.Description,
						ID = m.ID,
						IsDeleted = m.IsDeleted,
						MetaDescriptions = m.MetaDescriptions,
						MetaKeywords = m.MetaKeywords,
						MetaTitle = m.MetaTitle,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByUser = _UserService.GetUserById(m.ModifiedBy),
						PhotoImage = m.PhotoImage,
						CategoryID = m.CategoryID,
						ArticleCategory = m.CategoryID != null ? _ArticleCategoryService.GetArticleCategoryById(int.Parse(m.CategoryID.ToString())) : null,
						Details = m.Details,
						IsActive = m.IsActive,
						IsPopular = m.IsPopular,
						Tags = m.Tags,
						Title = m.Title,
						ViewCount = m.ViewCount
					})
					.ToList();

				paginationSet = new PaginationSet<Article>()
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
		/// Get Article by id
		/// </summary>
		/// <param name="id">ID of Article</param>
		/// <returns></returns>
		public ApiResponseModel GetById(int id)
		{
			var result = new Article();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _ArticleRepository.GetSingleById(id);
				if (exists != null && !exists.IsDeleted)
				{
					response.Result = GetArticleById(exists.ID);
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
		/// Get Article by id
		/// </summary>
		/// <param name="id">ID of Article</param>
		/// <returns></returns>
		public ArticleResponseModel GetArticleById(int id)
		{
			try
			{
				var m = _ArticleRepository.GetSingleById(id);
				if (m != null)
				{
					return new ArticleResponseModel
					{
						Created = m.Created,
						CreatedBy = m.CreatedBy,
						CreatedByUser = _UserService.GetUserById(m.CreatedBy),
						Description = m.Description,
						ID = m.ID,
						IsDeleted = m.IsDeleted,
						MetaDescriptions = m.MetaDescriptions,
						MetaKeywords = m.MetaKeywords,
						MetaTitle = m.MetaTitle,
						Modified = m.Modified,
						ModifiedBy = m.ModifiedBy,
						ModifiedByUser = _UserService.GetUserById(m.ModifiedBy),
						PhotoImage = m.PhotoImage,
						CategoryID = m.CategoryID,
						ArticleCategory = m.CategoryID != null ? _ArticleCategoryService.GetArticleCategoryById(int.Parse(m.CategoryID.ToString())) : null,
						Details = m.Details,
						IsActive = m.IsActive,
						IsPopular = m.IsPopular,
						Tags = m.Tags,
						Title = m.Title,
						ViewCount = m.ViewCount
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
		/// Add new Article
		/// </summary>
		/// <param name="obj">Article</param>
		/// <returns></returns>
		public ApiResponseModel Add(Article obj)
		{
			var result = new Article();
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				obj.ViewCount = 0;
				obj.IsDeleted = false;
				obj.Created = DateTime.Now;
				obj.CreatedBy = 1;//xxx
				obj.Modified = DateTime.Now;
				obj.ModifiedBy = 1;//xxx
				result = _ArticleRepository.Add(obj);
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
		/// Update Article
		/// </summary>
		/// <param name="obj">New Article</param>
		/// <returns></returns>
		public ApiResponseModel Update(Article obj)
		{
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};

			try
			{
				var exists = _ArticleRepository.GetSingleById(obj.ID);
				if (exists != null && !exists.IsDeleted)
				{
					exists.Title = obj.Title;
					exists.PhotoImage = obj.PhotoImage;
					exists.Description = obj.Description;
					exists.Details = obj.Details;
					exists.MetaTitle = obj.MetaTitle;
					exists.MetaDescriptions = obj.MetaDescriptions;
					exists.MetaKeywords = obj.MetaKeywords;
					exists.Tags = obj.Tags;
					exists.IsPopular = obj.IsPopular;
					exists.IsActive = obj.IsActive;
					exists.Modified = DateTime.Now;
					exists.ModifiedBy = 1;//xxx
					_ArticleRepository.Update(exists);
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
		/// Delete Article by Id
		/// </summary>
		/// <param name="id">ID of Article</param>
		/// <returns></returns>
		public ApiResponseModel Delete(int id)
		{
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};
			var exists = _ArticleRepository.GetSingleById(id);
			if (exists != null && !exists.IsDeleted)
			{
				response.Result = GetArticleById(id);
				exists.IsDeleted = true;
				_ArticleRepository.Update(exists);
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

		/// <summary>
		/// Update view count
		/// </summary>
		/// <param name="id">ID of Article</param>
		/// <returns></returns>
		public ApiResponseModel UpdateViewCount(int id)
		{
			var response = new ApiResponseModel
			{
				Code = CommonConstants.ApiResponseSuccessCode,
				Message = null,
				Result = null
			};
			var exists = _ArticleRepository.GetSingleById(id);
			if (exists != null && !exists.IsDeleted)
			{
				exists.ViewCount = exists.ViewCount + 1;
				_ArticleRepository.Update(exists);
				_UnitOfWork.Commit();
				response.Result = GetArticleById(id);
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