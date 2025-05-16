import { ComponentFixture, TestBed } from "@angular/core/testing";

import { PolicyDetailPage } from "./policy-detail.page";

describe("PolicyFormPage", () => {
  let component: PolicyDetailPage;
  let fixture: ComponentFixture<PolicyDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PolicyDetailPage],
    }).compileComponents();

    fixture = TestBed.createComponent(PolicyDetailPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
