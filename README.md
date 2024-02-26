# Overview

This project combines a couple of really cool technologies and structure and packages them into a neat,
re-usable template.

## Installing

To use this template, simply clone the repo.

In future I will set this up as a proper template.

# Structure

## Frontend

On the frontend, the project combines:

- Minimal API's: Using the [FastEndpoints](https://fast-endpoints.com/) library and the REPR pattern, we can register
  new endpoints very simply and in any location we like.
- Htmx: Hypermedia driven application, no client application state. We are using the Htmx.Net package from the
  wonderful [Khalid Abuhakmeh](https://khalidabuhakmeh.com/)
- Vite: As realists, we understand that sometimes we are required to use frontend dependencies. Vite serves as our
  module bundler, and provides hot reload functionality.
- TailwindCSS: To me, Tailwind is a high level API that wraps the lower level CSS. The inline nature is perfect for
  htmx.
- DaisyUI: Pretty much a simpler version of Bootstrap, but for Tailwind. And I think they do an awesome job of keeping
  the config intuitive and defaults clean. Its also really pretty.
- FontAwesomeIcons: They've just got really nice icons.

Why Razor views over Razor components? I don't quite get components. I tried to use them like jsx and it just was not
working. I will take another crack at it in future, because I think the idea of strongly typed components is just too
powerful not to consider. 

## Backend

I have many thoughts on enterprise level software architecture. I disagree with the conventional wisdom that every
single layer should be a different C# project, and that everything should be arranged by layers. Instead, I suggest the
old approach of functional core, imperative shell and the concept of driver and driven programs. This means our actual
application contains no state, and anything to do with state is injected as a dependency following
the Clean Architecture principles.

A driver program is one that has interaction with users, or is run an some kind of schedule, GUI apps, API's, automated
console apps, that kind of thing. These projects drive the Application. But since the application has no state and no
concrete implementations, it does nothing. It needs its dependencies injected by the driver program. We also divide the
project by Feature and group related functionality.

This may sounds similar to the Vertical Slices Jimmy Bogard proposes. While I agree with the majority of his logic,
integrating the dependencies with business logic is not sustainable for large project for a couple of reasons:

- Testing: How do we test the business logic independently of the implementation?
- Dependency changes: If we need to make a dependency change, we now have to change each feature, we have broken the
  dependency inversion principle.

Which brings me to my understanding of how we actually architecture the solution.

![Functional Core, Imperative Shell.png](Functional%20Core%2C%20Imperative%20Shell.png)

The project structure is then as follows:

1. __Domain__: Following DDD or parts of it, our domain is the lowest level. It is shared among all Features, and may
   even be shared across applications, hence it is its own project.
2. __Core__: This project is the actual application containing the business logic divided by feature. All logic that
   depends on databases, email, and other services rely on interfaces. This allows this project to be pure in the
   functional sense.
3. __Infrastructure__: Here, all our implementation details are managed. If we have an IEmailSender, we create an
   EmailSender with Mailkit that implements the interface. The responsibility of the implementation of these services is
   up to the driver program, but since this may grow very large, we have seperated it into its own project.
4. __Website__: This is our driver program. The architecure of this layer is its own discussion, but it follows roughly
   the same Feature slices design. Note that if your application is suitably simple, throw the rest of the projects
   away, register services in Program.cs, inject them into your handlers, and treat the handlers as the MediatR
   Commands/Queries.

# Credits

As you may be able to tell, this is an _opinionated_ template. Feel free to disagree with me. I really like the ideas
behind this project, and for my use cases they work really well.

I need to include some credits and thanks to people without whom this would not exist.

- __Khalid Abuhakmeh__ for his blog, I relied heavily on his content.
- __Jimmy Bogard__ for his talk on Vertical Slices
- __Matteo Contrini__ for his blog on Vite integration with .Net