using System;
using System.ComponentModel.DataAnnotations;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime date = (DateTime)value;
        return date >= DateTime.Now;
    }
}

public class NotOnWeekendAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime date = (DateTime)value;
        return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }
}

public class NotOnMondayAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime date = (DateTime)value;
        var model = (ConsultationFormModel)validationContext.ObjectInstance;

        if (model.SelectedProduct == "Основи" && date.DayOfWeek == DayOfWeek.Monday)
        {
            return new ValidationResult("Консультація не може бути в понеділок для продукту 'Основи'");
        }

        return ValidationResult.Success;
    }
}

public class ConsultationFormModel
{
    [Display(Name = "Ім'я та прізвище")]
    [Required(ErrorMessage = "Ім'я та прізвище обов'язкові для введення")]
    public string FullName { get; set; }

    [Display(Name = "Електронна пошта")]
    [Required(ErrorMessage = "Email обов'язковий для введення")]
    [EmailAddress(ErrorMessage = "Неправильний формат Email")]
    public string Email { get; set; }

    [Display(Name = "Дата консультації")]
    [Required(ErrorMessage = "Дата консультації обов'язкова для введення")]
    [DataType(DataType.DateTime)]
    [FutureDate(ErrorMessage = "Дата має бути в майбутньому")]
    [NotOnWeekend(ErrorMessage = "Консультація не може бути вихідним днем")]
    [NotOnMonday(ErrorMessage = "Консультація не може бути в понеділок для продукту 'Основи'")]
    public DateTime ConsultationDate { get; set; }

    [Display(Name = "Оберіть предмет для консультації")]
    [Required(ErrorMessage = "Оберіть предмет для консультації")]
    public string SelectedProduct { get; set; }
}

