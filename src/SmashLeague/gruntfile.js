/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
  grunt.initConfig({
    copy: {
      dev: {
        files: [
          {
            src: 'node_modules/bootstrap/dist/css/bootstrap.css',
            dest: 'wwwroot/lib/bootstrap/css/bootstrap.css',
            options: {
              expand: true,
              flatten: true
            }
          },
          {
            src: 'node_modules/bootstrap/dist/js/bootstrap.js',
            dest: 'wwwroot/lib/bootstrap/js/bootstrap.js',
            options: {
              expand: true,
              flatten: true
            }
          },
          {
            src: 'node_modules/jquery/dist/jquery.js',
            dest: 'wwwroot/lib/jquery/js/jquery.js',
            options: {
              expand: true,
              flatten: true
            }
          }
        ]
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-copy');
};