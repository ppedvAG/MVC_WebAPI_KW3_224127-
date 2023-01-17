# EF Core und Relationen:


# Welche Techniken gibt es um auf relationale Daten zu zugreifen?

## EAGER LOADING: Wir sagen expliziet, dass wir Relationale Datensätze mit reinladen wollen. 

Beispiel: siehe-> .Include(blog => blog.Posts)

using (var context = new BloggingContext())
{
    var blogs = context.Blogs
        .Include(blog => blog.Posts)
        .ToList();
}

 




## LAZY LOADING: Wird automatische alle Relationalen Daten mitgeladen. 


