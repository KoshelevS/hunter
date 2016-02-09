"use strict";

var gulp = require("gulp"),
    jshint = require('gulp-jshint'),
    concat = require("gulp-concat"),
    rename = require('gulp-rename'),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/"
};

paths.js = "./Scripts/**/*.js";
paths.css = "./Css/**/*.css";
paths.concatJsDest = paths.webroot + "js/site.js";
paths.concatCssDest = paths.webroot + "css/site.css";
paths.concatMinJsDest = paths.webroot + "js/site.min.js";
paths.concatMinCssDest = paths.webroot + "css/site.min.css";

// Линтинг файлов
gulp.task('lint', function () {
    gulp.src(paths.js)
      .pipe(jshint())
      .pipe(jshint.reporter('default'));
});

gulp.task("concat:js", function () {
    return gulp.src(paths.js)
        .pipe(concat(paths.concatJsDest))
        .pipe(gulp.dest("."));
});
gulp.task("concat:css", function () {
    return gulp.src(paths.css)
        .pipe(concat(paths.concatCssDest))
        .pipe(gulp.dest("."));
});
gulp.task("concat", ["concat:js", "concat:css"]);

gulp.task("min:js", function () {
    return gulp.src(paths.js)
        .pipe(concat(paths.concatMinJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});
gulp.task("min:css", function () {
    return gulp.src(paths.css)
        .pipe(concat(paths.concatMinCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});
gulp.task("min", ["min:js", "min:css"]);

gulp.task('default', function () {
    gulp.watch([paths.js, paths.css], ['lint', 'concat', 'min']);
});