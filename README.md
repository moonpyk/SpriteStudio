# SpriteGenerator

Initial awesomeness has been found at : [http://spritegenerator.codeplex.com/](http://spritegenerator.codeplex.com/)

All kudos to **csigusz** the original author. Huge thanks to **Jake Gordon** for the simple and fast bin packing argorithm.

# Changes 

   * New automatic layout builder, less efficient than the original one but more, more more fast and simple to maintain, see Jake Gordon's blog for [explanation](http://codeincomplete.com/posts/2011/5/7/bin_packing/) and [demo](http://codeincomplete.com/posts/2011/5/7/bin_packing/example/).
   * A library has been extracted from the code, multicore machines are first class citizens thanks to the [.NET Task Parallel Library](http://msdn.microsoft.com/en-us/library/dd460717.aspx).
   * Smaller CSS generated when all images have the same width and/or height.
   * The code has been refactored to follow MS code style guidelines (say ReSharper ones). 
   * The UI has been updated to be more responsive and non-blocking when working with large amount of files.

# Future plans
   * [WIP] New UI for better productivity.
   * [WIP] Add more options to customize the generated css.
   * Give possibility to output SASS/SCSS, Less CSS-preprocessors formats.
   * GTK# UI for Mono Users. **?**
   * Visual Studio integration **?**

# License

The original project was published under the MS-PL open-source license, my modifications will keep following this license.
