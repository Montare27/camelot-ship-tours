# «Camelot» Ship Tours ⛵️🌊

This is the backend project for Ship Tours, a .NET application built on the Onion architecture. The project consists of several modules, including Application, Domain, Api, Persistence, and Identity. The Identity module utilizes JWT tokens for authentication and authorization.

![image](https://github.com/Montare27/camelot-ship-tours/assets/117319414/e926c524-2523-4e95-92b8-779b15ddcd34)


## 👷 Architecture
Ship Tours follows the Onion architecture, which promotes separation of concerns and modular design. The different modules have distinct responsibilities, ensuring a clear separation of application logic, domain entities, data persistence, and API endpoints.

+ **[Application:](https://github.com/Montare27/camelot-ship-tours/tree/master/Application)** This module contains the application logic and serves as the entry point for interacting with the system. It encapsulates the use cases and orchestrates the interactions between the different layers of the architecture.

+ **[Domain:](https://github.com/Montare27/camelot-ship-tours/tree/master/Domain)** The domain module defines the core business entities, value objects, and business rules. It represents the heart of the application and contains the essential business logic, independent of any infrastructure or implementation details.

+ **[Api:](https://github.com/Montare27/camelot-ship-tours/tree/master/Api)** This module provides the RESTful API endpoints that allow external clients to interact with the system. It handles requests, validates input, and orchestrates the execution of the corresponding application use cases.

+ **[Persistence:](https://github.com/Montare27/camelot-ship-tours/tree/master/Persistence)** The persistence module is responsible for data storage and retrieval. It interacts with the database or any other data store, providing the necessary mechanisms to save and fetch domain objects.

+ **[Identity API:](https://github.com/Montare27/camelot-ship-tours/tree/master/Identity)** This module handles user authentication and authorization. It integrates JWT (JSON Web Tokens) for secure token-based authentication, allowing users to securely access protected resources within the application.

## 🚀 Getting Started
To set up the Ship Tours backend locally, follow these steps:

1. Clone the repository: git clone https://github.com/Montare27/camelot-ship-tours.git
2. Install the required dependencies using a package manager like NuGet.
3. Configure the connection string and any other necessary settings in the application configuration files.
4. Build the solution to ensure all dependencies are resolved correctly.
5. Run the application using your preferred method (e.g., Visual Studio, command-line).

## 🤝 Contributing
We welcome contributions to the Ship Tours project. If you want to contribute, please follow these guidelines:

1. Fork the repository and create a new branch for your feature or bug fix.
2. Make your changes and ensure they are properly tested.
3. Submit a pull request, clearly describing the changes you have made.
4. Your pull request will be reviewed, and feedback may be provided for further improvements.
5. Once approved, your changes will be merged into the main repository.

## 📄 License
The Ship Tours backend project is open source and released under the  <ins>MIT License </ins>.

## 📧 Contact
If you have any questions or suggestions regarding the Ship Tours project, feel free to contact us at dmitriytkachenko350@gmail.com. We appreciate your interest and support!

Let's set sail on the Ship Tours adventure together!⛵️🌊
