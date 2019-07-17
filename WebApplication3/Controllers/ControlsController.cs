using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication3.Common;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class ControlsController : Controller
    {
        [HttpGet]
        [ActionName(Constants.ACTION_WEBSITE)]
        public ActionResult Website()
        {
            return GetAllControlsView(false);
        }

        [HttpGet]
        [ActionName(Constants.ACTION_EDITOR)]
        public ActionResult Editor()
        {
            return GetAllControlsView(true);
        }

        [HttpGet]
        [ActionName(Constants.REDIS_KEY_TEXTBOX1)]
        public PartialViewResult GetTextbox1()
        {
            ViewBag.PostAction = Constants.REDIS_KEY_TEXTBOX1;
            ControlsModel controls = new ControlsModel();
            return PartialView(Constants.TEXTBOX_POPUP_PARTIAL_VIEW_NAME, controls.Textbox1);
        }

        [HttpPost]
        [ActionName(Constants.REDIS_KEY_TEXTBOX1)]
        public ActionResult UpdateTextbox1(TextboxModel textbox)
        {
            RedisService.Instance.SetValue<TextboxModel>(Constants.REDIS_KEY_TEXTBOX1, textbox);
            return GetAllControlsView(true);
        }

        [HttpGet]
        [ActionName(Constants.REDIS_KEY_TEXTBOX2)]
        public PartialViewResult GetTextbox2()
        {
            ViewBag.PostAction = Constants.REDIS_KEY_TEXTBOX2;
            ControlsModel controls = new ControlsModel();
            return PartialView(Constants.TEXTBOX_POPUP_PARTIAL_VIEW_NAME, controls.Textbox2);
        }

        [HttpPost]
        [ActionName(Constants.REDIS_KEY_TEXTBOX2)]
        public ActionResult UpdateTextbox2(TextboxModel textbox)
        {
            RedisService.Instance.SetValue<TextboxModel>(Constants.REDIS_KEY_TEXTBOX2, textbox);
            return GetAllControlsView(true);
        }

        [HttpGet]
        [ActionName(Constants.REDIS_KEY_DATETIME)]
        public PartialViewResult GetDatetimePicker()
        {
            ControlsModel controls = new ControlsModel();
            ViewBag.Items = GetSelectListForDatetimeTypes(controls);
            return PartialView(Constants.DATETIME_POPUP_PARTIAL_VIEW_NAME, controls.DatetimePicker);
        }

        [HttpPost]
        [ActionName(Constants.REDIS_KEY_DATETIME)]
        public ActionResult UpdateDatetime(DateTimeModel dateTime)
        {
            RedisService.Instance.SetValue<DateTimeModel>(Constants.REDIS_KEY_DATETIME, dateTime);
            return GetAllControlsView(true);
        }

        [HttpGet]
        [ActionName(Constants.REDIS_KEY_BUTTON)]
        public PartialViewResult GetButton()
        {
            ControlsModel controls = new ControlsModel();
            return PartialView(Constants.BUTTON_POPUP_PARTIAL_VIEW_NAME, controls.Button);
        }

        [HttpPost]
        [ActionName(Constants.REDIS_KEY_BUTTON)]
        public ActionResult UpdateButton(ButtonModel button)
        {
            RedisService.Instance.SetValue<ButtonModel>(Constants.REDIS_KEY_BUTTON, button);
            return GetAllControlsView(true);
        }

        private ViewResult GetAllControlsView(bool isEditable)
        {
            ViewBag.Title = Constants.TITLE;
            ViewBag.Editable = isEditable;
            ControlsModel controls = new ControlsModel();
            return View(Constants.CONTROLS_VIEW_NAME, controls);
        }

        private System.Collections.IEnumerable GetSelectListForDatetimeTypes(ControlsModel controls)
        {
            SelectList items = new SelectList(new List<SelectListItem>()
            {
                new SelectListItem { Value = Constants.DATETIME_LOCAL_VALUE, Text = Constants.DATETIME_LOCAL_TEXT, Selected = (controls.DatetimePicker.Type == Constants.DATETIME_LOCAL_VALUE)},
                new SelectListItem { Value = Constants.DATE_VALUE, Text = Constants.DATE_TEXT, Selected = (controls.DatetimePicker.Type == Constants.DATE_VALUE) },
            });
            return items.Items;
        }
    }
}