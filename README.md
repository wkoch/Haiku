haiku
=====

haiku is a simple and small cli static blog generator.

Run the command below to learn how to use it.

~~~
ruby haiku.rb help
~~~

New posts will be saved on the **drafts** folder, you can manually move them to the **posts** folder or use specific commands (**publish** or **publish all**) to publish them.
All files on the **posts** folder will always be published when running:

~~~
ruby haiku.rb
~~~

All html files will be created on the project's root, that way you can easily keep you website and haiku project in the same github repo.
