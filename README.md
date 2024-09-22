[<img src="https://img.icons8.com/?size=512&id=55494&format=png" align="right" width="25%" padding-right="350">]()

# `ISSUETRACKER`

#### <code> Software to track issues in your projects</code>

<p align="left">
	<img src="https://img.shields.io/github/license/Leplik500/IssueTracker?style=flat&logo=opensourceinitiative&logoColor=white&color=0080ff" alt="license">
	<img src="https://img.shields.io/github/last-commit/Leplik500/IssueTracker?style=flat&logo=git&logoColor=white&color=0080ff" alt="last-commit">
	<img src="https://img.shields.io/github/languages/count/Leplik500/IssueTracker?style=flat&color=0080ff" alt="repo-language-count">
</p>
<p align="left">
		<em>Built with the tools and technologies:</em>
</p>
<p align="left">
	<img src="https://img.shields.io/badge/Dotnet-512BD4.svg?style=flat&logo=Dotnet&logoColor=white" alt="Dotnet">
  <img src="https://img.shields.io/badge/PostgreSQL-4169E1.svg?style=flat&logo=PostgreSQL&logoColor=white" alt="PostgreSQL">
  <img src="https://img.shields.io/badge/Redis-FF4438.svg?style=flat&logo=Redis&logoColor=white" alt="Redis">
	<img src="https://img.shields.io/badge/Docker-2496ED.svg?style=flat&logo=Docker&logoColor=white" alt="Docker">
	<img src="https://img.shields.io/badge/JavaScript-F7DF1E.svg?style=flat&logo=JavaScript&logoColor=black" alt="JavaScript">
	<img src="https://img.shields.io/badge/YAML-CB171E.svg?style=flat&logo=YAML&logoColor=white" alt="YAML">
	<img src="https://img.shields.io/badge/JSON-000000.svg?style=flat&logo=JSON&logoColor=white" alt="JSON">
</p>

<br>

#####  Table of Contents

- [ Overview](#-overview)
- [ Features](#-features)
- [ Repository Structure](#-repository-structure)
- [ Modules](#-modules)
- [ Getting Started](#-getting-started)
    - [ Prerequisites](#-prerequisites)
    - [ Installation](#-installation)
    - [ Usage](#-usage)
    - [ Tests](#-tests)
- [ Project Roadmap](#-project-roadmap)
- [ Contributing](#-contributing)
- [ License](#-license)
- [ Acknowledgments](#-acknowledgments)

---

##  Overview

<code>  This is learning project which goal is to learn to create webapps with .NET Core MVC, as well as learning how to use corresponding technologies, such as HTML, CSS, Javascript, PostgreSQL, Redis, Entity Framework, LINQ, SignalR, REST API, Docker, NGINX.</code>

---

##  Features

- CRUD operations with users and issues
- Replacing :shortcodes: of emojis to corresponding symbols in issues (with use of third-party API)
- Real-time comments in issues

---

##  Repository Structure

```sh
└── IssueTracker/
    ├── IssueTracker
    ├── IssueTracker.DAL
    ├── IssueTracker.Domain
    ├── IssueTracker.Service
    ├── IssueTracker.sln
    ├── IssueTracker.sln.DotSettings
    ├── IssueTracker.sln.DotSettings.user
    ├── docker-compose.yml
    ├── global.json
    └── nginx.conf
```
##  Getting Started

###  Prerequisites

**CSharp**: `version x.y.z`

###  Installation

Build the project from source:

1. Clone the IssueTracker repository:
```sh
❯ git clone https://github.com/Leplik500/IssueTracker
```

2. Navigate to the project directory:
```sh
❯ cd IssueTracker
```

3. Install the required dependencies:
```sh
❯ dotnet build
```

###  Usage

To run the project, execute the following command:

```sh
❯ dotnet run
```

###  Tests

Execute the test suite using the following command:

```sh
❯ dotnet test
```

---

##  Project Roadmap

- [X] **`Task 1`**: <strike>Implement feature one.</strike>
- [ ] **`Task 2`**: Implement feature two.
- [ ] **`Task 3`**: Implement feature three.

---

##  Contributing

Contributions are welcome! Here are several ways you can contribute:

- **[Report Issues](https://github.com/Leplik500/IssueTracker/issues)**: Submit bugs found or log feature requests for the `IssueTracker` project.
- **[Submit Pull Requests](https://github.com/Leplik500/IssueTracker/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.
- **[Join the Discussions](https://github.com/Leplik500/IssueTracker/discussions)**: Share your insights, provide feedback, or ask questions.

<details closed>
<summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your github account.
2. **Clone Locally**: Clone the forked repository to your local machine using a git client.
   ```sh
   git clone https://github.com/Leplik500/IssueTracker
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Make Your Changes**: Develop and test your changes locally.
5. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
6. **Push to github**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
7. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.
8. **Review**: Once your PR is reviewed and approved, it will be merged into the main branch. Congratulations on your contribution!
</details>

<details closed>
<summary>Contributor Graph</summary>
<br>
<p align="left">
   <a href="https://github.com{/Leplik500/IssueTracker/}graphs/contributors">
      <img src="https://contrib.rocks/image?repo=Leplik500/IssueTracker">
   </a>
</p>
</details>

---

##  License

This project is protected under the [SELECT-A-LICENSE](https://choosealicense.com/licenses) License. For more details, refer to the [LICENSE](https://choosealicense.com/licenses/) file.

---

##  Acknowledgments

- List any resources, contributors, inspiration, etc. here.

---
