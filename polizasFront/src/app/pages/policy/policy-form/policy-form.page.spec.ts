import { ComponentFixture, TestBed } from "@angular/core/testing";

import { PolicyFormPage } from "./policy-form.page";

describe("PolicyFormPage", () => {
  let component: PolicyFormPage;
  let fixture: ComponentFixture<PolicyFormPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PolicyFormPage],
    }).compileComponents();

    fixture = TestBed.createComponent(PolicyFormPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
