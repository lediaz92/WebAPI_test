
La Api inicia en: 
GET: (localhost)/api/posts --> Obtiene todos los post segun el perfil del usuario logueado (al principio al no estar ningun usuario logueado solo devuelve los aprobados y activos)

Para obtener un solo post por ID:
GET: (localhost)/api/posts/{ID} --> Obtiene un solo post que se muestra o no segun el perfil del usuario 

Para obtener un post por Titulo:
GET: (localhost)/api/Posts/GetPostsByTitle/{title} --> Obtiene un solo post que se muestra o no segun el perfil del usuario 

Para desactivar un post:
PUT: (localhost)/api/Posts/DeactivatePosts/{ID} --> solo el perfil de editor puede desactivar un post

Para activar un post:
PUT: (localhost)/api/Posts/ActivatePosts/{ID} --> solo el perfil de editor puede activar un post

Para aprobar un post
PUT: (localhost)/api/Posts/ApprovePosts/{ID} --> solo el perfil de editor puede aprobar un post

Para rechazar un post
PUT: (localhost)/api/Posts/RejectPosts/{ID} --> solo el perfil de editor puede rechazar un post

para editar un post:
PUT: (localhost)/api/posts/EditPosts/{ID} --> solo el perfil de escritor puede editar, requiere enviar en el cuerpo un JSON con los cambios en el titulo o texto. Ejemplo:{"title": "TituloEditado", "text": "TextoEditado"}

Para crear un post:
POST: (localhost)/api/posts --> solo el perfil de escritor puede crear, requiere enviar en el cuerpo un JSON con los parametros de titulo y texto. Ejemplo:{"title": "TituloNuevo", "text": "TextoNuevo"}

Para eliminar un post:
DELETE: (localhost)/api/Posts/{ID} --> solo el perfil de editor puede eliminar un post

Para loguearse:
GET: (localhost)/api/users/login/{usuario}/{contraseña}

Para desloguearse:
GET: (localhost)/api/users/logout

Para ver comentarios:
GET: (localhost)/api/Comments --> Obtiene todos los comentarios activos y aprobados. Solo el editor puede ver todos

Para obtener un solo comentario por ID:
GET: (localhost)/api/Comments/{ID} --> Obtiene un solo comentario que este activo y aprobado segun ID. Solo el editor puede ver todos

Para obtener los comentarios segun el Post:
GET: (localhost)/api/Comments/GetCommentByPostID/{ID} --> Obtiene los comentarios solo cuando tanto el post como los comentarios estan aprobados y activos. Solo el editor puede ver todos

Para Crear un comentario:
POST: (localhost)/api/Comments --> solo los usuarios no logueados pueden crear comentarios, requiere enviar en el cuerpo un JSON con los parametros de comentario, nombre y post. Ejemplo {"Comment":"ComentarioNuevo", "UserName": "Lector1","PostID": "5E63656E-18EA-EA11-BBC0-74D435EE802A"}

Para Eliminar un comentario:
DELETE: (localhost)/api/Comments/{id} --> Solo el perfil de editor puede eliminar un comentario

	
