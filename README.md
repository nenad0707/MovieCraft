# 🎬 MovieCraft - Blazor Hosted Movie Application

**MovieCraft** is a **Blazor Hosted** application designed to provide a seamless experience for browsing and managing your favorite movies. The project incorporates a modular architecture with distinct layers, following **Clean Architecture** principles. It leverages **Azure Active Directory B2C** for authentication and **TMDb API** for fetching movie data such as trailers, release dates, and more.

## 🚀 **Live Demo**: [Visit MovieCraft](https://nenadtara-001-site3.jtempurl.com/)

## 📖 Table of Contents

- [🎬 MovieCraft - Blazor Hosted Movie Application](#-moviecraft---blazor-hosted-movie-application)
  - [📖 Table of Contents](#-table-of-contents)
  - [📚 Project Overview](#-project-overview)
  - [✨ Features](#-features)
  - [🛠️ Tech Stack \& Tools](#️-tech-stack--tools)
    - [Frontend](#frontend)
    - [Backend](#backend)
    - [Authentication](#authentication)
    - [DevOps \& Deployment](#devops--deployment)
  - [🔍 Architecture Overview](#-architecture-overview)
    - [Layers Overview](#layers-overview)
  - [🎯 Key Components and Design Decisions](#-key-components-and-design-decisions)
  - [🔐 Authentication with Azure AD B2C](#-authentication-with-azure-ad-b2c)
  - [💻 Frontend Design Details](#-frontend-design-details)
    - [Modular and Declarative Components](#modular-and-declarative-components)
    - [Glassmorphism Effect in UI](#glassmorphism-effect-in-ui)
    - [3D Coverflow Carousel](#3d-coverflow-carousel)
  - [📄 License](#-license)

---

## 📚 Project Overview

MovieCraft is a full-featured movie browsing app where users can:

- **Browse Popular Movies** retrieved from **TMDb API**.
- **Search for Movies** by title.
- **Watch Trailers** of movies directly within the app.
- **Add Movies to Favorites** and manage your personalized favorites list.
- Enjoy a **dynamic background** that changes based on the selected movie.

The project is structured as a **Blazor Hosted** application, separating the **Client**, **Server**, and **Shared** components while ensuring high scalability and maintainability.

---

## ✨ Features

- **Dynamic Movie Search**: Instantly search for movies with real-time results fetched from **TMDb API**.
- **Favorites Management**: Users can securely log in and manage their favorite movies.
- **Trailer Playback**: Watch trailers for movies directly through an integrated modal.
- **Responsive UI**: Built with a mobile-first approach, ensuring great user experience on both desktop and mobile devices.
- **Blazored Toast Notifications**: Visual feedback through notifications for actions like adding favorites or errors.
- **Dynamic Backgrounds**: Background images adjust based on the selected movie for an immersive experience.

---

## 🛠️ Tech Stack & Tools

### Frontend

- **Blazor WebAssembly**: Client-side logic and UI components written in C#.
- **CSS & BoxIcons**: Beautifully styled interface with icons and animations.
- **Blazored Toast**: Toast notifications for success/error feedback.
- **TMDb API**: Integration with TMDb API for fetching movie data like posters, trailers, etc.
- **Swiper.js**: 3D Coverflow effect for the movie carousel, creating a modern and visually appealing layout.
- **Glassmorphism Effect**: Used in navigation and layout for a sleek, modern UI experience.

### Backend

- **ASP.NET Core**: Backend services with REST API to handle user favorites and other server-side logic.
- **Entity Framework Core**: Database management for persisting user data, movie lists, and more.
- **MediatR & CQRS**: Ensures clean separation of concerns between commands and queries.

### Authentication

- **Azure Active Directory B2C**: Secure, scalable, and easy-to-use authentication for user management.

### DevOps & Deployment

- **Docker**: Containerized for easy deployment across environments.
- **GitHub Actions**: CI/CD for automated deployment to hosting platform.
- **SmarterASP.NET**: Hosted on **SmarterASP.NET** for live demonstration.

---

## 🔍 Architecture Overview

MovieCraft follows **Clean Architecture** principles, ensuring a separation of concerns and maintaining flexibility for future improvements. The project is structured in the following layers:

### Layers Overview

1. **MovieCraft.Client**: The Blazor WebAssembly front-end where the user interacts with the app.
2. **MovieCraft.Server**: The backend API that processes data, connects to databases, and handles business logic.
3. **MovieCraft.Shared**: Shared models and DTOs between client and server to ensure type safety and reduce duplication.
4. **MovieCraft.Application**: Contains the core business logic, including the **MediatR** handlers for CQRS operations.
5. **MovieCraft.Infrastructure**: Manages external services, including **Entity Framework** for database interactions and **Azure AD B2C** for authentication.
6. **MovieCraft.Domain**: Contains domain models and aggregates for the application's core entities.

---

## 🎯 Key Components and Design Decisions

1. **Blazor Hosted Architecture**: The application is divided into **Client**, **Server**, and **Shared** projects. This provides a clear separation between frontend and backend, making the app highly scalable and maintainable.

2. **Clean Architecture**: The project strictly follows Clean Architecture, with separation of concerns between core business logic, external services, and the presentation layer. CQRS pattern is implemented using **MediatR**, ensuring a clean flow of commands and queries.

3. **Azure AD B2C**: Provides a highly secure and scalable authentication solution, allowing users to log in seamlessly and manage their own favorite movies list.

4. **TMDb API Integration**: The app fetches real-time movie data from TMDb, including posters, trailers, and detailed movie descriptions.

5. **Responsive Design**: The application is fully responsive, ensuring a great experience on both mobile and desktop devices.

---

## 🔐 Authentication with Azure AD B2C

MovieCraft uses **Azure Active Directory B2C** to handle user authentication. Setting this up ensures a smooth and secure login experience for users. Here’s a simplified breakdown of how **Azure AD B2C** is integrated into the application:

1. **Azure AD B2C Setup**: To use this service, you need to set up your **Azure AD B2C** tenant. Follow the [Azure AD B2C Setup Guide](https://learn.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-tenant) to create your tenant and configure your policies.

2. **Application Registration**: Once your tenant is ready, register two applications:

- **Server API Application**: Used by the backend to validate tokens and manage user information.
- **Client Application**: Used by the frontend Blazor WebAssembly client to authenticate users and obtain access tokens.

3. **Configure Redirect URIs**: Ensure you have proper **Redirect URIs** for both local development (`https://localhost:5001`) and production.

4. **AppSettings Configuration**: Add your **Tenant ID**, **Client ID**, and **Scopes** to the `appsettings.json` file for both the Client and Server projects.

5. **Testing the Flow**: Once configured, test the login flow to ensure that users can log in, manage their favorites, and log out securely.

For more detailed instructions on how to configure **Azure AD B2C**, refer to the [official documentation](https://learn.microsoft.com/en-us/azure/active-directory-b2c/).

---

## 💻 Frontend Design Details

### Modular and Declarative Components

MovieCraft uses a modular approach for all UI components, ensuring they can be reused and adapted for different parts of the application. Components such as **ActionButtons**, **SearchInput**, and **Carousel** are implemented declaratively, making the codebase clean and easy to maintain.

### Glassmorphism Effect in UI

The **NavMenu** and **MainLayout** components implement a **Glassmorphism** design, providing a modern and sleek look. This effect is achieved using CSS properties like **backdrop-filter** and **blur**, combined with subtle shadows and transparency. It adds a layer of sophistication to the app's visual identity, making the interface both functional and visually engaging.

### 3D Coverflow Carousel

The movie browsing experience is enhanced with a 3D **Coverflow** effect in the **Carousel** component, achieved using **Swiper.js**. This feature not only looks impressive but also adds an intuitive interaction model for users to explore popular movies. The combination of **Blazor** and **JavaScript Interop** (JS Interop) ensures smooth integration of this advanced UI feature.

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE.txt](LICENSE.txt) file for details.

---

Thank you for checking out **MovieCraft**! 🎥🍿 If you enjoyed the project or found it helpful, please consider giving it a ⭐️ on GitHub!
