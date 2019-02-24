# File upload dans une API ASP.NET Core 2.1

Ce projet illustre l'upload de fichiers dans une API ASP.NET Core 2.1. 

Voir la classe PhotosController et le helper MultiPartRequestHelper. 

Remarques: 
- lisez la documentation officielle. Ce projet illustre l'approche "Streaming" permettant de supporter des fichiers volumineux
- Le fichier uploadé est pour le moment simplement stocké sur le file system du serveur dans un emplacement temporaire. Vous pourriez vouloir le stocker dans un fournisseur plus spécialisé (ex: Cloudinary). Voir wiki pour un pointeur vers une solution de ce type. 

Les ressources de référence utilisées sont:
- https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-2.1 (documentation officielle mais incomplète)
- https://github.com/aspnet/Docs/tree/master/aspnetcore/mvc/models/file-uploads/sample/FileUploadSample (voir StreamingController)
- https://dotnetcoretutorials.com/2017/03/12/uploading-files-asp-net-core/ 