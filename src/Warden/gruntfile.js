/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.initConfig({
        uglify: {
            my_target: {
                options: {
                    beautify: true
                },
                files: { 'wwwroot/app.js': ['app/app.js', 'app/**/*.js'] }
            }
        },

        watch: {
            scripts: {
                files: ['app/**/*.js'],
                tasks: ['uglify']
            }
        },
        copy: {
            files: {
                    cwd: 'wwwroot/lib/bootstrap/fonts',
                    dest: 'wwwroot/fonts',
                    src: '**',
                    expand: true
            }
        }
    });

    grunt.registerTask('default', ['uglify', 'watch']);
    
};