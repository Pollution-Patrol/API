# **Pollution Patrol**

Pollution Patrol is a web application designed to help volunteers and organizations fight pollution by locating and dealing with polluted zones. The app allows users to report polluted areas, find and join volunteer organizations, and track the progress of cleanup efforts. Pollution Patrol is built using .NET technologies, specifically ASP.NET Core 7 and MS SQL for backend API.

## **Features**

- User registration and authentication: Users can sign up for the app using their email or social media accounts, and verify their identity using two-factor authentication or other security measures.
- Reporting polluted areas: Users can report polluted areas by submitting photos, videos, and descriptions of the location and severity of the pollution. They can add geolocation data to their reports, or manually select the location on a map.
- Reviewing and approving reports: Real employees or volunteers can review and approve reports to ensure their authenticity and accuracy. They can leave comments or feedback on the reports, and escalate or reject reports that do not meet the app's guidelines or standards.
- Finding and joining volunteer organizations: Users can search for and join volunteer organizations that work to clean up polluted areas, or create their own organizations and invite others to join.
- Exploring and filtering reports on a map: Users can explore the map of polluted areas and filter the reports based on location, severity, or date. They can see the number of reports and the average severity level for each area, and click on individual reports to see more details and comments.
- Earn rewards and social impact: Users can earn rewards, badges, or points for submitting reports, joining organizations, or participating in events or activities. They can track their progress and see the social impact of their contributions, such as the number of areas cleaned up or the amount of donations raised.
- Advanced features using machine learning and AI: The app can use machine learning and AI to identify patterns and trends in pollution data, and provide personalized recommendations and solutions to users and organizations. It can also use AI to predict potential future pollution hotspots or identify areas that are most in need of immediate attention.

## **Modules**

The app is designed with a modular monolithic architecture, consisting of the following modules:

1. User Access: Handles every type of operation related to user, beginning from registration application, changing password or email, deleting an account, and hashing passwords, etc.
2. Report: Stores reports submitted by users, including geolocation data, photos or videos of evidence, descriptions, and contact information.
3. Review: Allows real employees or volunteers to review and approve reports, and leave comments or feedback.
4. Organization: Stores volunteer organizations and groups, allowing users to search for and join organizations, and for organizations to post events, activities, and news updates to their members.
5. Admin: Uses by stuff.

## **Installation**

To run the Pollution Patrol app locally, you will need to follow these steps:

1. Clone the repository to your local machine.
2. Open the project in your preferred IDE, such as Visual Studio.
3. Install the necessary dependencies, including ASP.NET Core 7 and MS SQL.
4. Set up the database and run the necessary migrations for each module.
5. Configure the secrets used in the app by using the .NET secret tool. To do this, use 'Example.Secret.Json' file.
6. Start the server and navigate to the local address in your web browser.

## **Contributing**

We welcome contributions to Pollution Patrol from developers of all skill levels. If you have an idea for a new feature or improvement, or have found a bug that needs fixing, please create a new issue in the GitHub repository. You can also submit a pull request with your changes, and we will review and merge it if it meets our standards.

## **License**

Pollution Patrol is released under the **[MIT License](https://opensource.org/licenses/MIT)**, which means that you can use, copy, modify, distribute, and sublicense the app for both commercial and non-commercial purposes, as long as you include the original copyright notice and license terms. However, the app is provided "as is" without any warranty, and the developers will not be liable for any damages or losses resulting from the use or inability to use the app.

## Contact

If you have any questions, comments, or feedback about Pollution Patrol, please feel free to contact us at **[support@pollutionpatrol.com](mailto:support@pollutionpatrol.com)**. We appreciate your interest and support in our mission to fight pollution and make the world a cleaner and safer place.
