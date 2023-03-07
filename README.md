# Timetable Planning App
Web application for working with scheduling of model railway operation at module meetings.
The application will be integrated with the [Module Registry](https://moduleregistry.azurewebsites.net).

## Goal
To build a web and cloud based scheduling system for multiuse and multi tenancy, and
to make all source code and other assets open source.

## Plan as of March 2023
The development of the Timetable Planning App is started.
It is in its initial phase and the preview will be updated when new features are available.
Other projects will be in my focus during 2023, so we'll see when I can make progress.

The printing functionality will be based on earlier development. Take a look at [the examples](Examples).

If you have questions, comments or ideas, please file them at the GitHub discussion page.

## Function overview
1. Defining layout topology: stations and their connecting stretches.
2. Defining scheduling stretches: the working unit and the unit for grapghical timetables.
2. Scheduling trains on a stretch with extensive checking of consistency and other scheduling rules.
4. Building loco- trainset, and cargo flow scheduling by defining the trains or part of train they should run.
5. Constructing driver duties from one or several loco schedule parts.
6. Printing of all types of documentation needed.
7. Automatic creation of driver instructions based on scheduling data.
8. Automatic creation of instructions for train dispatcher and shunting staff.
9. Option for manually writing driver instructions in several languages.
10. User authorization.


## Environment
The Timetable Planning App will initially run locally in your browser, 
and you save the schedule to the cloud.

However, Microsoft will make it quite easy to create a standalone local app that runs on Windows and Mac. 
Then you will have an option to save to cloud or locally. 
This technology will also be finally released in November this year. 

Note that the app will require a large screen and a mouse.
It will not work well on small screens, like phones or some tablets.

Printing to PDF will be supported, including correct paginatnation for booklet printing.

The app will work integrated with the [Module Registry App](https://moduleregistry.azurewebsites.net/).
The data will be stored in the same database, 
and the user authentication and authorization will be the same.

## Contribute
I you are interested of scheduling of model trains, especially for module meetings, you can contribute in many ways:
1. Follow the project with happy shouts!
2. Give feedback on the idea and the documentation.
3. Engage in design of functionality.
4. Help with translations; add new or improve existing ones.
5. Submit issues and suggestions.
6. Contribute with pull requests.

All interaction takes place on GitHub and we use en English only.
So you will need a GitHub account and apply for membership in the project.
You find more details in the [Contribution page](CONTRIBUTING.md).

Welcome to participate!
