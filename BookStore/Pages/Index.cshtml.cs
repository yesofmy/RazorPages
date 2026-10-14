using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public Book Book { get; set; } = new();

    public SelectList Publishers { get; set; } = null!;

    public string? SuccessMessage { get; set; }


    private readonly List<Publisher> PublisherList = new()
    {
        new Publisher
        {
            Id = 1,
            Name = "Penguin Books"
        },

        new Publisher
        {
            Id = 2,
            Name = "HarperCollins"
        },

        new Publisher
        {
            Id = 3,
            Name = "Macmillan Publishers"
        }
    };


    public void OnGet()
    {
        Publishers = new SelectList(
            PublisherList,
            "Id",
            "Name"
        );
    }


    public void OnPost()
    {
        Publishers = new SelectList(
            PublisherList,
            "Id",
            "Name"
        );


        Publisher? selectedPublisher =
            PublisherList.FirstOrDefault(
                publisher => publisher.Id == Book.PublisherId
            );


        if (selectedPublisher != null)
        {
            SuccessMessage =
                $"Book added successfully: {Book.Title} ({selectedPublisher.Name})";
        }
    }
}