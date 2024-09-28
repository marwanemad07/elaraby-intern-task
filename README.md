# Online Shopping Website Backend

This repository contains the backend implementation of an Online Shopping Website built using .NET Core 8 and following the N-Tier architecture. The application was developed as part of my internship at Elaraby Group and includes functionalities such as user registration, product listing, and order processing. The system is connected to a SQL Server database and uses Entity Framework for data access.

Both the backend and frontend of this project are deployed. You can find the live version of the backend [here](https://elarabyintern.runasp.net), and the frontend is deployed on [Vercel](https://elaraby-intern-task.vercel.app/products.html).

## Features

- **User Registration & Login**  
  Allows users to register and log in using Microsoft Membership for authentication.

- **User Roles**  
  There are two user types:
  - **Admin**: Can add new products to the platform.
  - **User**: Can browse products, add items to the cart, and place orders.
  
- **Product Listing**  
  Retrieve a list of available products for purchase.

- **Product Details**  
  Retrieve detailed information about a specific product, including name, price, and description.

- **Shopping Cart**  
  Users can add products to their shopping cart and view the items in their cart.

- **Order Processing**  
  Users can place orders for items in their cart, and order confirmations are generated.

- **Authentication & Authorization**  
  JWT is used to secure endpoints, ensuring that only authenticated users can access certain functionalities like order processing, and only admins can add new products.

- **Error Handling & Validation**  
  Includes proper error handling and validation to manage invalid requests and provide meaningful error messages.

- **Security**  
  Protects sensitive user data and mitigates common security vulnerabilities, such as SQL injection and XSS.

- **Scalability & Performance**  
  Optimized for efficient handling of large numbers of users and products.

## Technologies Used

- **Backend**: .NET Core 8
- **Database**: SQL Server with Entity Framework
- **Authentication**: Microsoft Membership & JWT
- **Architecture**: N-Tier Architecture
- **Frontend Deployment**: Vercel
- **Backend Deployment**: Monster ASP.NET
- **API Documentation**: Swagger
- **Database Migrations & Seeding**: Implemented using Entity Framework

### API Documentation

The API documentation is available through Swagger. You can explore the API endpoints, input parameters, and responses by navigating to `/swagger/index.html`.

### Frontend

The frontend of this project was developed using HTML, CSS, and JavaScript. It is deployed on Vercel and interacts with the backend APIs.
