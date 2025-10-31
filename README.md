# Karaokay - Karaoke Application

A modern C# WPF desktop application for generating and playing karaoke versions of any song.

## Features

- **Modern Dark UI**: Beautiful, rounded interface matching your design mockup
- **Song Library**: Browse and search through your karaoke collection
- **Filter Options**: View all songs, favorites, or recently added tracks
- **Song Details Panel**: Display song information, thumbnail, and metadata
- **Playback Controls**: Play button and progress slider (ready for audio integration)
- **Custom Window Controls**: Minimize, maximize, and close buttons

## Project Structure

```
Karaokay/
├── Karaokay.sln                 # Visual Studio solution file
└── Karaokay/
    ├── Karaokay.csproj          # Project file
    ├── App.xaml                 # Application resources and styles
    ├── App.xaml.cs              # Application code-behind
    ├── MainWindow.xaml          # Main window UI
    ├── MainWindow.xaml.cs       # Main window logic
    ├── Models/
    │   └── Song.cs              # Song data model
    └── Services/
        └── SongService.cs       # Song management service
```

## Requirements

- .NET 8.0 or later
- Windows OS
- Visual Studio 2022 (recommended) or Visual Studio Code with C# extension

## Getting Started

### 1. Build the Project

Open a terminal in the project directory and run:

```powershell
cd Karaokay
dotnet restore
dotnet build
```

### 2. Run the Application

```powershell
dotnet run
```

Or open `Karaokay.sln` in Visual Studio and press F5.

## UI Components

### Left Panel - Song List
- **Search Bar**: Filter songs by title, artist, or genre
- **Filter Buttons**: 
  - All: Show all songs
  - Favorites: Show only favorited songs
  - Recent: Show recently added songs
- **Song Items**: Display thumbnail, title, artist, and duration

### Right Panel - Song Details
- **Title**: Currently selected song title
- **Thumbnail**: Song artwork/thumbnail
- **Metadata**: Artist, duration, and genre information
- **Playback Controls**: Play button and progress slider

## Integration Points for Your Model

The application is designed to be easily integrated with your karaoke generation model:

### 1. Audio Playback
In `MainWindow.xaml.cs`, the `PlayButton_Click` method is where you'll integrate audio playback:

```csharp
private void PlayButton_Click(object sender, RoutedEventArgs e)
{
    // TODO: Add your audio playback logic here
    // Example: Load the karaoke track and play it
}
```

### 2. Song Import
The `MenuButton_Click` method is where you can add functionality to import songs and generate karaoke versions:

```csharp
private void MenuButton_Click(object sender, RoutedEventArgs e)
{
    // TODO: Add song import and karaoke generation
    // 1. Let user select audio file
    // 2. Send to your model for vocal removal
    // 3. Add to song library
}
```

### 3. Progress Tracking
The `ProgressSlider_ValueChanged` method can be used to implement seek functionality:

```csharp
private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
{
    // TODO: Implement seek when audio is playing
}
```

## Customization

### Adding New Songs
Songs are currently loaded from sample data in `SongService.cs`. To add real songs:

1. Modify the `LoadSampleData()` method or create a new method to load from a database/file
2. Update the `Song` model if you need additional properties
3. Implement file import functionality in the menu

### Styling
The application uses Material Design themes. You can customize colors in `App.xaml`:

```xml
<materialDesign:BundledTheme BaseTheme="Dark" PrimaryColor="DeepPurple" SecondaryColor="Lime" />
```

### Window Appearance
The window has a custom borderless design with rounded corners. Modify `MainWindow.xaml` to adjust:
- Border radius
- Colors
- Spacing
- Layout

## Next Steps

1. **Audio Integration**: Add audio playback using NAudio or similar library
2. **Model Integration**: Connect your karaoke generation model
3. **Database**: Implement persistent storage for songs
4. **File Import**: Add functionality to import audio files
5. **Lyrics Display**: Add synchronized lyrics display during playback
6. **Export**: Allow exporting generated karaoke tracks

## Dependencies

- **MaterialDesignThemes**: For modern UI components and icons
- **MaterialDesignColors**: Color themes for Material Design

## License

This project is created for your personal karaoke application.

## Support

For issues or questions about the frontend implementation, refer to the code comments and this README.
