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

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(ArticleCategory ArticleCategory);

        ApiResponseViewModel Update(ArticleCategory ArticleCategory);

        ApiResponseViewModel Delete(int id);

    }

    public class ArticleCategoryService : IArticleCategoryService
    {
        private IArticleCategoryRepository _ArticleCategoryRepository;

        private IUnitOfWork _UnitOfWork;

        public ArticleCategoryService(IArticleCategoryRepository ArticleCategoryRepository, IUnitOfWork UnitOfWork)
        {
            this._ArticleCategoryRepository = ArticleCategoryRepository;
            this._UnitOfWork = UnitOfWork;
        }

        /// <summary>
        /// Get all ArticleCategory
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<ArticleCategory>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _ArticleCategoryRepository.GetAll().Where(m => m.IsDeleted != true).ToList();
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
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<ArticleCategory>();
            var paginationSet = new PaginationSet<ArticleCategory>();
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
                var query = _ArticleCategoryRepository.GetAll().Where(m => m.IsDeleted != true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
					.ToList();

                paginationSet = new PaginationSet<ArticleCategory>()
                {
                    Items = result,
                    Page = currentPage,
                    TotalCount = result.Count(),
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
        public ApiResponseViewModel GetById(int id)
        {
            var result = new ArticleCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _ArticleCategoryRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    response.Result = _ArticleCategoryRepository.GetSingleById(id);
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
        /// Add new ArticleCategory
        /// </summary>
        /// <param name="obj">ArticleCategory</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(ArticleCategory obj)
        {
            var result = new ArticleCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
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
        public ApiResponseViewModel Update(ArticleCategory obj)
        {
            var result = new ArticleCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _ArticleCategoryRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _ArticleCategoryRepository.Update(obj);
                    _UnitOfWork.Commit();
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
        public ApiResponseViewModel Delete(int id)
        {
            var result = new ArticleCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _ArticleCategoryRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _ArticleCategoryRepository.Delete(id);
                    _UnitOfWork.Commit();
                    response.Message = CommonConstants.DeleteSuccess;
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
    }
}