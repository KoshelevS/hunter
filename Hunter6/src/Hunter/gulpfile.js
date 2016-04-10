/// <binding ProjectOpened='default' />
"use strict";

var gulp = require("gulp"),
    jshint = require('gulp-jshint'),
    concat = require("gulp-concat"),
    sass = require('gulp-sass'),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    template = require('gulp-template'),
    rename = require('gulp-rename'),
    inject = require('gulp-inject-string'),
    appsettings = require("./appsettings.json");

var paths = {
    webroot: "./wwwroot/"
};

paths.js = "./Scripts/**/*.js";
paths.templateJs = "./Scripts/**/*.template.js";
paths.excludeJs = "!" + paths.templateJs;
paths.css = "./Styles/**/*.scss";
paths.concatJsDest = paths.webroot + "js/site.js";
paths.concatCssDest = paths.webroot + "css/site.css";
paths.concatMinJsDest = paths.webroot + "js/site.min.js";
paths.concatMinCssDest = paths.webroot + "css/site.min.css";

gulp.task('template:js', function () {
    gulp.src(paths.templateJs, {base: "./"})
      .pipe(template({ "SettingsJSON": JSON.stringify(appsettings.Common) }))
      .pipe(rename(function (path) {
          path.basename = path.basename.substring(0, path.basename.lastIndexOf(".template"));
       }))
      .pipe(gulp.dest("."));
});

// Линтинг файлов
gulp.task('lint', function () {
    gulp.src([paths.js, paths.excludeJs])
      .pipe(jshint())
      .pipe(jshint.reporter('default'));
});

gulp.task("concat:js", function () {
    return gulp.src([paths.js, paths.excludeJs])
        .pipe(concat(paths.concatJsDest))
        .pipe(inject.prepend('"use strict";\n'))
        .pipe(gulp.dest("."))
        .pipe(rename(paths.concatMinJsDest))
        .pipe(uglify())
        .pipe(gulp.dest('.'));
});

gulp.task("concat:css", function () {
    return gulp.src(paths.css)
        .pipe(sass().on('error', sass.logError))
        .pipe(concat(paths.concatCssDest))
        .pipe(gulp.dest("."))
        .pipe(rename(paths.concatMinCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});
gulp.task("concat", ["concat:js", "concat:css"]);

gulp.task('default', function () {
    gulp.watch([paths.js, paths.css], ['template:js','lint', 'concat']);
});