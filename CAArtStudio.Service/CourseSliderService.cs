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
    public interface ICourseSliderService
    {

        ApiResponseViewModel GetAll();

        ApiResponseViewModel GetAllWithPagging(int? page, int pageSize);

        ApiResponseViewModel GetById(int id);

        ApiResponseViewModel Add(CourseSlider CourseSlider);

        ApiResponseViewModel Update(CourseSlider CourseSlider);

        ApiResponseViewModel Delete(int id);

    }

    public class CourseSliderService : ICourseSliderService
    {
        private ICourseSliderRepository _CourseSliderRepository;

        private IUnitOfWork _UnitOfWork;

        public CourseSliderService(ICourseSliderRepository CourseSliderRepository, IUnitOfWork UnitOfWork)
        {
            this._CourseSliderRepository = CourseSliderRepository;
            this._UnitOfWork = UnitOfWork;
        }

        /// <summary>
        /// Get all CourseSlider
        /// </summary>
        /// <returns></returns>
        public ApiResponseViewModel GetAll()
        {
            var result = new List<CourseSlider>();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _CourseSliderRepository.GetAll().Where(m => m.IsDeleted != true).ToList();
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
        /// Get all CourseSlider with pagging
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public ApiResponseViewModel GetAllWithPagging(int? page, int pageSize)
        {
            var result = new List<CourseSlider>();
            var paginationSet = new PaginationSet<CourseSlider>();
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
                var query = _CourseSliderRepository.GetAll().Where(m => m.IsDeleted != true);
                totalRow = query.Count();

                result = query.OrderBy(x => x.ID).Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
					.ToList();

                paginationSet = new PaginationSet<CourseSlider>()
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
        /// Get CourseSlider by id
        /// </summary>
        /// <param name="id">ID of CourseSlider</param>
        /// <returns></returns>
        public ApiResponseViewModel GetById(int id)
        {
            var result = new CourseSlider();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _CourseSliderRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    response.Result = _CourseSliderRepository.GetSingleById(id);
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
        /// Add new CourseSlider
        /// </summary>
        /// <param name="obj">CourseSlider</param>
        /// <returns></returns>
        public ApiResponseViewModel Add(CourseSlider obj)
        {
            var result = new CourseSlider();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                result = _CourseSliderRepository.Add(obj);
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
        /// Update CourseSlider
        /// </summary>
        /// <param name="obj">New CourseSlider</param>
        /// <returns></returns>
        public ApiResponseViewModel Update(CourseSlider obj)
        {
            var result = new CourseSlider();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {
                var exists = _CourseSliderRepository.CheckContains(m => m.ID == obj.ID);
                if (exists)
                {
                    _CourseSliderRepository.Update(obj);
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
        /// Delete CourseSlider by Id
        /// </summary>
        /// <param name="id">ID of CourseSlider</param>
        /// <returns></returns>
        public ApiResponseViewModel Delete(int id)
        {
            var result = new CourseSlider();
            var response = new ApiResponseViewModel
            {
                Code = CommonConstants.ApiResponseSuccessCode,
                Message = null,
                Result = null
            };

            try
            {

                var exists = _CourseSliderRepository.CheckContains(m => m.ID == id);
                if (exists)
                {
                    result = _CourseSliderRepository.Delete(id);
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