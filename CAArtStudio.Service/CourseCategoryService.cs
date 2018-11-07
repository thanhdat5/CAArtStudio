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
    public interface ICourseCategoryService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(CourseCategory CourseCategory);

        ApiResponseViewModel Update(CourseCategory CourseCategory);

        ApiResponseViewModel Delete(int id);

    }

    public class CourseCategoryService : ICourseCategoryService
    {
        private ICourseCategoryRepository _CourseCategoryRepository;

        private IUnitOfWork _UnitOfWork;

        public CourseCategoryService(ICourseCategoryRepository CourseCategoryRepository, IUnitOfWork UnitOfWork)
        {
            this._CourseCategoryRepository = CourseCategoryRepository;
            this._UnitOfWork = UnitOfWork;
        }

        /// <summary>
        /// Get all CourseCategory
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<CourseCategory>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _CourseCategoryRepository.GetAll().Where(m => m.IsDeleted != true).ToList();
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
        /// Get all CourseCategory with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<CourseCategory>();
            var paginationSet = new PaginationSet<CourseCategory>();
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
                var query = _CourseCategoryRepository.GetAll().Where(m => m.IsDeleted != true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
					.ToList();

                paginationSet = new PaginationSet<CourseCategory>()
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
        /// Get CourseCategory by id
        /// </summary>
        /// <param name="id">ID of CourseCategory</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new CourseCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _CourseCategoryRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    response.Result = _CourseCategoryRepository.GetSingleById(id);
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
        /// Add new CourseCategory
        /// </summary>
        /// <param name="obj">CourseCategory</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(CourseCategory obj)
        {
            var result = new CourseCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _CourseCategoryRepository.Add(obj);
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
        /// Update CourseCategory
        /// </summary>
        /// <param name="obj">New CourseCategory</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(CourseCategory obj)
        {
            var result = new CourseCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _CourseCategoryRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _CourseCategoryRepository.Update(obj);
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
        /// Delete CourseCategory by Id
        /// </summary>
        /// <param name="id">ID of CourseCategory</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new CourseCategory();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _CourseCategoryRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _CourseCategoryRepository.Delete(id);
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