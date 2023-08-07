using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PlayPal.MVC.Models.BoardGameView
{
    public class BoardGameCreateView
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Number of copies is required")]
        public int NumberOfCopies { get; set; }

        [Required(ErrorMessage = "Activity status is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public Guid SelectedCategory { get; set; }

        public List<CategoryView>  AllCategories { get; set; }    
    }
}