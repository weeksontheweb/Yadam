# Yadam #

## What Is It? ##

I want to create self hosted software that allows myself
and my family to create albums and upload photos/videos to
them.

This may go nowhere, but it's an interesting project and I can
learn a lot from it, so it's more of a study exercise.

## The Mechanics ##

This is written in C# .Net 6 MVC.

I started developing using PostgresQL as a data store,
but then shortly moved to using SQL Server. The rationale
behind this is that I really thought it best to stay
within the Microsoft ecosystem and I have a lot more
experience in SQL Server than PostgresQL.
However, there is the start of code for using PostgresQL.
However as the data store objects are injected, swapping
between one and the other will be simple

Data connection is performed by using Dapper. I could
have chosen 'Entity Framework', but thought it might be
overkill for the size of project. Also I'm planning to run
this application in a docker container, so a lightweight
model like Dapper is more beneficial, especially when
running on a 'Lenovo M93P'.

One of the reasons I chose Postgres initially, was that the
images are more lightweight and I therefore thought
that I would get better performance. However as I wanted
to get the project developed more quickly, I opted for
the slightly heavier SQL Server image and see what the performance
will be like, and switch later if I need to.

## Future Plans ##

- Have some authentication and permissions so that Family
  members can sign in and:
    - Create/Delete albums.
    - Add/Delete album items (down to item level granularity).
    - Download albums/photos/videos/
- Allow the option of streaming videos, rather than having
  to just download them.

## Signing Off ##

As mentioned before, development will be continued over
time with improvements to the existing software.