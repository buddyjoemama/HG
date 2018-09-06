/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        uglify: {
            build_dev: {
                options: {
                    sourceMap: false,
                    beautify: true,
                    mangle: false,
                    compress: false,
                    preserveComments: 'all'
                },
                files: {
                    'dist/web.hg.min.js': ['Scripts/application/**/*.js', 'Components/**/*.js'],
                    'dist/web.hg.libs.min.js': [
                        'Scripts/jquery/jquery-3.3.1.min.js',
                        'Scripts/angular/angular.min.js',
                        'Scripts/angular/angular-route.min.js',
                        'Scripts/angular/angular-animate.min.js',
                        'Scripts/angular/angular-sanitize.min.js',
                        'Scripts/angular/angular-messages.min.js',
                        'Scripts/angular-ui/ui-bootstrap-tpls.js',
                        'Scripts/moment/moment.min.js'
                    ]
                }
            }
        },

        watch: {
            all: {
                files: ['Components/**/*.js'],
                tasks: ['dev']
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('dev', 'uglify:build_dev');
};