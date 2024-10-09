![title2](https://github.com/user-attachments/assets/d0561ea4-f9ec-44e7-add3-62fea2d1cac3)

# Ghost Tracker
## Description
Ghost Tracker is an **UNOFFICIAL** assistant application for the game Phasmophobia. It provides detailed information about game mechanics, ghosts, and much more. The app delivers up-to-date information on game events and news, and includes maps of game locations with markings for hiding places, cursed possessions, and fuse box locations. Additionally, the app features a system to assist in identifying ghosts.

This application is not affiliated with or endorsed by the official developers of Phasmophobia, Kinetic Games, or any other related entities. All content within the app is sourced from publicly available materials and is intended for informational purposes only.

## Features
- **Ghost Information**: Detailed descriptions and characteristics of each ghost type.
- **Equipment Information**: Detailed description and characteristics of each equipment item.
- **Maps Information**: Detailed description of each location in the game with a detailed map of that location, hiding spots and locations of cursed possessions.
- **Cursed Possession Information**: Detailed descriptions and characteristics of each cursed possession.
- **Evidence Information**: Detailed description and characteristics of each evidence type.
- **Difficulty Information**: Detailed description and characterestics of each difficulty type.
- **Challenge Mode Information**: Detailed information about the features of each challenge mode, the list of provided equipment and the selected map.
- **Achievement Information**: Detailed information about each achievement that is in the game and a little hint on how to get it.
- **Task Information**: Detailed description of each weekly and daily task and a little tip on how to complete it.
- **Other Information**: Detailed information about the various mechanics of the game.
- **News**: Several recent news items from the game's steam page, with links to those news items.
- **Current Weekly Challenge Mode**: Information on what the current challenge mode is this week.
- **Current Tasks**: Information about the current weekly and daily tasks in the game.
- **Ghost Guess Helper**: System to assist in identifying a ghost based on available evidence, ghost speed, and secondary signs.
- **Tip**: Random tips on various aspects of the game.

## Technologies
This application leverages a range of modern technologies and libraries to ensure high performance and a robust user experience:

- **Programming Language**: C#.
- **Framework**: Xamarin Forms 5.0, used for building cross-platform mobile applications with a single codebase.
- **Architecture**: Follows the Model-View-ViewModel (MVVM) pattern to separate the user interface from business logic, ensuring clean and manageable code.
- **Development Environment**: Visual Studio 2022.

### Key Libraries and Packages:
- **Entity Framework Core**: Utilized for ORM with SQLite to manage relational data more effectively.
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.Sqlite`
- **Newtonsoft.Json**: Handles JSON data manipulation and serialization.
- **Rg.Plugins.Popup**: Provides pop-up functionality for interactive user interfaces.
- **Serilog**: Advanced logging framework that supports logging to various storage types and protocols.
  - `Serilog.Sinks.File` for file logging.
  - `Serilog.Sinks.Http` for HTTP logging.
- **Xamarin Community Toolkit & Xamarin.Essentials**: Offer a collection of common functionality and cross-platform APIs for iOS and Android.
- **Xamarin.Forms.PancakeView**: Used for enhanced styling and layout options beyond the standard Xamarin Forms controls.

### Platform Target:
- Specifically designed for Android devices, optimizing the app for use on a wide range of screens and device types.

## Future Plans
Ghost Tracker is committed to continuous improvement and expansion. Here are some enhancements and features planned for future releases:

- **Additional Language Support**: Building on the existing multilingual support, the app will be translated into additional languages to further expand accessibility.
- **Theme Customization**: Introducing multiple themes to enable users to personalize their app experience.
- **iOS Support**: Plans to develop and release an iOS version of Ghost Tracker, expanding the user base to include Apple device users.
- **Ongoing Improvements**: Enhancements will be made based on user feedback to continually refine and optimize the functionality and user experience of Ghost Tracker.

## Acknowledgements

Thank you to the Phasmophobia communities on Reddit and Discord, as well as the creators and contributors to the game's Wiki for their efforts in gathering and organizing information. Special thanks to everyone who published various information about the game that was used in this app: Ethyriel, FearlessJames, SnoopaDD, User-undetected0, and all others whose names I may have missed (if this happened, please contact me to rectify this). These open resources were invaluable in the creation of this application and helped ensure the accuracy of the data provided. I would also like to express my gratitude to the developers of all the open libraries used in the project. **Special thanks to the game developers for a fantastic game and for inspiring the creation of this application.**

## Contact

To get in touch, please use the following email address: [vireslumen.dev@gmail.com](mailto:vireslumen.dev@gmail.com).

## Screenshots
<table style="border-collapse: collapse; border: none;">
  <tr>
    <td align="center"   width="300"><img src="https://github.com/user-attachments/assets/7a4f0189-8925-4c1e-9baa-0637c8bc8d38" width="200" alt="Main Page"/><br><em>Main Tab</em></td>
    <td align="center"   width="300"><img src="https://github.com/user-attachments/assets/1819f43e-c115-4e78-a93b-40d174d098bb" width="200" alt="Main Page 2"/><br><em>Pop-up tooltip</em></td>
    <td align="center"   width="300"><img src="https://github.com/user-attachments/assets/32e923d2-ef85-4e55-b2aa-d283f7db135d" width="200" alt="Main Page 3"/><br><em>Bottom of the Main Tab</em></td>
  </tr>
  <tr>
    <td align="center" ><img src="https://github.com/user-attachments/assets/94f0cbcf-7482-4884-944f-90c032bb0b07" width="200" alt="Ghosts Page"/><br><em>Ghosts Tab</em></td>
    <td align="center" ><img src="https://github.com/user-attachments/assets/34741139-c3d2-4f95-a642-177ae44a46a1" width="200" alt="Equipments Page"/><br><em>Equipments Tab</em></td>
    <td align="center" ><img src="https://github.com/user-attachments/assets/100e798b-6d02-4e46-8faa-8cd03e9b0180" width="200" alt="Maps Page"/><br><em>Maps Tab</em></td>
    <td align="center" width="300"><img src="https://github.com/user-attachments/assets/25d7f6e8-fb29-4dff-8a0b-5106474fa615" width="200" alt="Cursed Possessions Page"/><br><em>Cursed Possessions Tab</em></td>
  </tr>
  <tr>
    <td align="center" ><img src="https://github.com/user-attachments/assets/56753088-1980-46de-87d4-1186db7e2622" width="200" alt="Detail Map Page"/><br><em>Detail Map Page</em></td>
    <td align="center" ><img src="https://github.com/user-attachments/assets/fb6aee52-d4e5-49e0-8428-f2e4e2019a94" width="200" alt="Ghost Guess Page"/><br><em>Challenge Mode Detail Page</em></td>
    <td align="center" ><img src="https://github.com/user-attachments/assets/480203e1-b436-4711-8df2-33ecf3379c6c" width="200" alt="Ghost Guess Page"/><br><em>Ghost Guess Helper Page</em></td>
  </tr>
</table>
