using WebApplication3.Common;
using WebApplication3.Services;

namespace WebApplication3.Models
{
    public class ControlsModel
    {
        public TextboxModel Textbox1 { get; set; }
        public TextboxModel Textbox2 { get; set; }
        public DateTimeModel DatetimePicker { get; set; }
        public ButtonModel Button { get; set; }

        public ControlsModel()
        {
            Textbox1 = GetTextboxModel(Constants.REDIS_KEY_TEXTBOX1);
            Textbox2 = GetTextboxModel(Constants.REDIS_KEY_TEXTBOX2);
            DatetimePicker = GetDatetimeModel();
            Button = GetButtonModel();
        }

        private TextboxModel GetTextboxModel(string key)
        {
            TextboxModel textboxModel = RedisService.Instance.GetValue<TextboxModel>(key);
            return textboxModel ?? GetDefaultTextbox();
        }

        private TextboxModel GetDefaultTextbox()
        {
            TextboxModel textboxModel = new TextboxModel
            {
                BorderColor = Constants.DEFAULT_BORDER_COLOR,
                TextColor = Constants.DEFAULT_TEXT_COLOR,
                DefaultText = Constants.DEFAULT_TEXT
            };
            return textboxModel;
        }

        private DateTimeModel GetDatetimeModel()
        {
            DateTimeModel dateTimeModel = RedisService.Instance.GetValue<DateTimeModel>(Constants.REDIS_KEY_DATETIME);
            return dateTimeModel ?? GetDefaultDatetime();
        }

        private DateTimeModel GetDefaultDatetime()
        {
            DateTimeModel dateTimeModel = new DateTimeModel
            {
                TextColor = Constants.DEFAULT_TEXT_COLOR,
                Type = Constants.DATETIME_LOCAL_VALUE
            };
            return dateTimeModel;
        }

        private ButtonModel GetButtonModel()
        {
            ButtonModel buttonModel = RedisService.Instance.GetValue<ButtonModel>(Constants.REDIS_KEY_BUTTON);
            return buttonModel ?? GetDefaultButton();
        }

        private ButtonModel GetDefaultButton()
        {
            ButtonModel buttonModel = new ButtonModel
            {
                BorderColor = Constants.DEFAULT_BORDER_COLOR,
                TextColor = Constants.DEFAULT_TEXT_COLOR,
                Text = Constants.DEFAULT_TEXT
            };
            return buttonModel;
        }
    }
}