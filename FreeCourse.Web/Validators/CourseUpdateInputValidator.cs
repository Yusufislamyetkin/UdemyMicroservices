﻿using FluentValidation;
using FreeCourse.Web.Models.Catalogs;

namespace FreeCourse.Web.Validators
{
    public class CourseUpdateInputValidator:AbstractValidator<CourseUpdateInput>
    {
        public CourseUpdateInputValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("isim alanı boş olmamalı");
            RuleFor(c => c.Description).NotEmpty().WithMessage("açıklama alanı boş olmamalı");
            RuleFor(c => c.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("süre alanı boş olmamalı");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori alanı seçiniz");

            // $$$$.$$
            RuleFor(c => c.Price).NotEmpty().ScalePrecision(2, 6).WithMessage("fiyat alanı boş olmamalı");
        }
    }
}
