/// <binding AfterBuild='less:dev' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
  grunt.initConfig({
    copy: {
      dev: {
        files: [
          { // bootstrap less
            expand: true,
            flatten: true,
            src: 'node_modules/bootstrap/less/*.less',
            dest: 'Less/bootstrap/',
          },
          { // bootstrap less mixins
            expand: true,
            flatten: true,
            src: 'node_modules/bootstrap/less/mixins/*.less',
            dest: 'Less/bootstrap/mixins/',
          },
          { // bootstrap js
            src: 'node_modules/bootstrap/dist/js/bootstrap.js',
            dest: 'wwwroot/lib/bootstrap/js/bootstrap.js',
          },
          { // jquery
            src: 'node_modules/jquery/dist/jquery.js',
            dest: 'wwwroot/lib/jquery/js/jquery.js',
          },
          { // font-awesome css
            expand: true,
            flatten: true,
            src: ['node_modules/font-awesome/css/*'],
            dest: 'wwwroot/lib/font-awesome/css/',
          },
          { // font-awesome fonts
            expand: true,
            flatten: true,
            src: ['node_modules/font-awesome/fonts/*'],
            dest: 'wwwroot/lib/font-awesome/fonts/'
          },
          { // angular
            expand: true,
            flatten: true,
            src: [
              'node_modules/angular/angular.js',
              'node_modules/angular/angular.min.js',
              'node_modules/angular/angular.min.js.map',
            ],
            dest: 'wwwroot/lib/angular/js/'
          },
          { // angular-ui-router
            expand: true,
            flatten: true,
            src: ['node_modules/angular-ui-router/release/*'],
            dest: 'wwwroot/lib/angular-ui-router/js/'
          },
          { // octicons
            expand: true,
            flatten: true,
            src: [
              'node_modules/octicons/octicons/*',
              '!node_modules/octicons/octicons/*.md',
              '!node_modules/octicons/octicons/*.scss',
              '!node_modules/octicons/octicons/*.less',
            ],
            dest: 'wwwroot/lib/octicons/'
          }
        ]
      }
    },

    less: {
      dev: {
        files: {
          'wwwroot/css/smash-league.css': 'Less/smash-league.less'
        }
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-less');
};