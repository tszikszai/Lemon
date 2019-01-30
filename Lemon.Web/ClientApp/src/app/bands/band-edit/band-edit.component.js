"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var range_validator_directive_1 = require("../../shared/range-validator.directive");
var lower_than_or_equal_to_validator_directive_1 = require("../../shared/lower-than-or-equal-to-validator.directive");
var BandEditComponent = /** @class */ (function () {
    function BandEditComponent(route, router, fb, bandService, messageService) {
        this.route = route;
        this.router = router;
        this.fb = fb;
        this.bandService = bandService;
        this.messageService = messageService;
        this.currentYear = new Date().getFullYear();
        this.band = {};
    }
    BandEditComponent.prototype.ngOnInit = function () {
        this.messageService.clear();
        this.createForm();
        var id = +this.route.snapshot.paramMap.get('id');
        if (id) {
            this.isEditing = true;
            this.getBand(id);
        }
        else {
            this.isEditing = false;
        }
    };
    BandEditComponent.prototype.getBand = function (id) {
        var _this = this;
        this.bandService.getBand(id)
            .subscribe(function (band) {
            _this.band = band;
            _this.updateForm();
        });
    };
    BandEditComponent.prototype.createForm = function () {
        this.form = this.fb.group({
            name: ['', [
                    forms_1.Validators.required,
                    forms_1.Validators.maxLength(100)
                ]
            ],
            activeFromYear: ['', [
                    forms_1.Validators.required,
                    forms_1.Validators.pattern(/^\d*$/),
                    range_validator_directive_1.rangeValidator(1900, this.currentYear)
                ]
            ],
            activeToYear: ['', [
                    forms_1.Validators.pattern(/^\d*$/),
                    range_validator_directive_1.rangeValidator(1900, this.currentYear)
                ]
            ]
        }, { validators: lower_than_or_equal_to_validator_directive_1.lowerThanOrEqualToValidator('activeFromYear', 'activeToYear') });
    };
    BandEditComponent.prototype.updateForm = function () {
        this.form.setValue({
            name: this.band.name,
            activeFromYear: this.band.activeFromYear,
            activeToYear: this.band.activeToYear || ''
        });
    };
    BandEditComponent.prototype.onSubmit = function () {
        var _this = this;
        this.messageService.clear();
        var band = this.createBandFromFormValues();
        if (this.isEditing) {
            this.bandService.updateBand(band)
                .subscribe(function () { return _this.navigateToBandList(); });
        }
        else {
            this.bandService.addBand(band)
                .subscribe(function () { return _this.navigateToBandList(); });
        }
    };
    BandEditComponent.prototype.createBandFromFormValues = function () {
        var band = {};
        if (this.isEditing) {
            band.id = this.band.id;
        }
        band.name = this.form.value.name;
        band.activeFromYear = this.form.value.activeFromYear;
        band.activeToYear = this.form.value.activeToYear;
        return band;
    };
    BandEditComponent.prototype.onCancel = function () {
        this.navigateToBandList();
    };
    BandEditComponent.prototype.onDelete = function () {
        var _this = this;
        if (!confirm('Do you really want to delete this band?')) {
            return;
        }
        this.messageService.clear();
        this.bandService.deleteBand(this.band.id)
            .subscribe(function () { return _this.navigateToBandList(); });
    };
    BandEditComponent.prototype.navigateToBandList = function () {
        this.router.navigate(['/bands']);
    };
    BandEditComponent.prototype.getFormControl = function (name) {
        return this.form.get(name);
    };
    BandEditComponent.prototype.hasError = function (name) {
        var control = this.getFormControl(name);
        return control && control.invalid && (control.dirty || control.touched);
    };
    BandEditComponent.prototype.getError = function (name, errorCode) {
        var control = this.getFormControl(name);
        return control.getError(errorCode);
    };
    BandEditComponent = __decorate([
        core_1.Component({
            selector: 'app-band-edit',
            templateUrl: './band-edit.component.html'
        })
    ], BandEditComponent);
    return BandEditComponent;
}());
exports.BandEditComponent = BandEditComponent;
//# sourceMappingURL=band-edit.component.js.map